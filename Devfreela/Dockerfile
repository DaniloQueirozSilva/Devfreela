#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Devfreela/DevFreela.csproj", "Devfreela/"]
COPY ["DevFreela.Application/DevFreela.Application.csproj", "DevFreela.Application/"]
COPY ["DevFreela.Core/DevFreela.Core.csproj", "DevFreela.Core/"]
COPY ["DevFreela.Infrastructure/DevFreela.Infrastructure.csproj", "DevFreela.Infrastructure/"]
RUN dotnet restore "./Devfreela/DevFreela.csproj"
COPY . .
WORKDIR "/src/Devfreela"
RUN dotnet build "./DevFreela.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevFreela.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevFreela.dll"]