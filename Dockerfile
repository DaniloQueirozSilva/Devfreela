FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install --global dotnet-ef
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Devfreela/DevFreela.API.csproj", "Devfreela/"]
COPY ["DevFreela.Application/DevFreela.Application.csproj", "DevFreela.Application/"]
COPY ["DevFreela.Core/DevFreela.Core.csproj", "DevFreela.Core/"]
COPY ["DevFreela.Infrastructure/DevFreela.Infrastructure.csproj", "DevFreela.Infrastructure/"]
RUN dotnet restore "./Devfreela/DevFreela.API.csproj"
COPY . .
WORKDIR "/src/Devfreela"

RUN dotnet build "./DevFreela.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
ENTRYPOINT ["dotnet", "ef database update"]

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevFreela.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false




FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevFreela.API.dll"]
