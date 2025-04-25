FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kollectionized.Api/Kollectionized.Api.csproj", "Kollectionized.Api/"]
RUN dotnet restore "Kollectionized.Api/Kollectionized.Api.csproj"
COPY . .
WORKDIR "/src/Kollectionized.Api"
RUN dotnet build "./Kollectionized.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kollectionized.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kollectionized.Api.dll"]
