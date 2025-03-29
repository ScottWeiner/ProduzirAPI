# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY ["ProduzirAPI.sln", "."]
COPY ["ProduzirAPI.csproj", "."]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the files
COPY . .

# Build and publish
RUN dotnet build "ProduzirAPI.csproj" -c Release -o /app/build
RUN dotnet publish "ProduzirAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5227
ENTRYPOINT ["dotnet", "ProduzirAPI.dll"]