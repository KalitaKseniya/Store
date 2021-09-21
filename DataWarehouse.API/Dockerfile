#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataWarehouse.API/DataWarehouse.API.csproj", "DataWarehouse.API/"]
RUN dotnet restore "DataWarehouse.API/DataWarehouse.API.csproj"
COPY . .
WORKDIR "/src/DataWarehouse.API"
RUN dotnet build "DataWarehouse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataWarehouse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataWarehouse.API.dll"]