#  Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

# Main Variable
ARG PROJECT_NAME=Api

# Directory setup
WORKDIR /src

# Ignoring test folders for build
COPY ${PROJECT_NAME}/${PROJECT_NAME}.csproj ./${PROJECT_NAME}/
COPY Domain/Domain.csproj ./Domain/
COPY Application/Application.csproj ./Application/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY TokenService/TokenService.csproj ./TokenService/

# Restore de packages
RUN dotnet restore ${PROJECT_NAME}/${PROJECT_NAME}.csproj

# Copy the rest of the files (look at .dockerignore to see what is being ignored)
COPY . .

#publish the project
RUN dotnet publish ${PROJECT_NAME}/${PROJECT_NAME}.csproj \
    -c Release -o /app/publish \
    --no-restore

#  stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final

WORKDIR /app

# Copia arquivos da etapa anterior
COPY --from=build /app/publish .


EXPOSE 80


#  Add dummy JWT variables so app doesn't crash on startup
# (App Runner will not have your appsettings.json by default)
ENV Jwt__Key="temporary_secret"
ENV Jwt__Issuer="apprunner"
ENV Jwt__Audience="apprunner"

# Move SQLite DB to writable area (/tmp)
ENV ConnectionStrings__Default="Data Source=/tmp/app.db"

# 5 Add health check for AWS App Runner
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD wget -qO- http://localhost:80/health || exit 1

# ------------------------------------------------------
# Run the app
# ------------------------------------------------------
ENTRYPOINT ["dotnet", "Api.dll"]