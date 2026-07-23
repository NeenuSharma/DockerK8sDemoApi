FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# IMPORTANT: replace "DockerK8sDemoApi.csproj" below with your ACTUAL .csproj filename
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
# Force the app to listen on plain HTTP inside the container (no HTTPS cert here)
ENV ASPNETCORE_URLS=http://+:8080

# IMPORTANT: replace with your actual output DLL name (usually matches your .csproj name)
CMD ["dotnet", "DockerK8sDemoApi.dll"]
