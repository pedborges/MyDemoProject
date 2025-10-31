# syntax=docker/dockerfile:1
ARG BUILD_CONFIGURATION=Release
ARG PROJECT_NAME=Api
ARG DOTNET_VERSION=9.0 

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src
COPY MyDemoProject.sln ./
COPY Api/Api.csproj Api/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY TokenService/TokenService.csproj Infrastructure/
RUN dotnet restore MyDemoProject.sln
COPY . .
RUN dotnet publish ./${PROJECT_NAME}/${PROJECT_NAME}.csproj \
    -c $BUILD_CONFIGURATION -o /out \ --no-restore 

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS final
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet","Api.dll"]