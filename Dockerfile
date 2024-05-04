FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5134

# remove this 2 if no longer want to access swagger
ENV ASPNETCORE_URLS http://*:80 
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=development
WORKDIR /src
COPY ["Jiran.csproj", "./"]
RUN dotnet restore "Jiran.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Jiran.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=development
RUN dotnet publish "Jiran.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jiran.dll"]

# Test