FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files
COPY src/PantryCloud.HouseholdService.Presentation/PantryCloud.HouseholdService.Presentation.csproj src/PantryCloud.HouseholdService.Presentation/
COPY src/PantryCloud.HouseholdService.Application/PantryCloud.HouseholdService.Application.csproj src/PantryCloud.HouseholdService.Application/
COPY src/PantryCloud.HouseholdService.Core/PantryCloud.HouseholdService.Core.csproj src/PantryCloud.HouseholdService.Core/
COPY src/PantryCloud.HouseholdService.Infrastructure/PantryCloud.HouseholdService.Infrastructure.csproj src/PantryCloud.HouseholdService.Infrastructure/

# Restore NuGet packages
RUN dotnet restore src/PantryCloud.HouseholdService.Presentation/PantryCloud.HouseholdService.Presentation.csproj

# Copy the rest of the source code
COPY . .

COPY secrets /secrets

# Build 
WORKDIR /src/src/PantryCloud.HouseholdService.Presentation
RUN dotnet build PantryCloud.HouseholdService.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish PantryCloud.HouseholdService.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PantryCloud.HouseholdService.Presentation.dll"]