FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj và restore dependencies
COPY WarehouseAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY WarehouseAPI/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Tạo thư mục cho SQLite database
RUN mkdir -p /app/data
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "WarehouseAPI.dll"]