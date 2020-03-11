#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        zlib1g \
        fontconfig \
        libfreetype6 \
        libx11-6 \
        libxext6 \
        libxrender1 \
    && curl -o /usr/lib/libwkhtmltox.so \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/raw/v1.0.8/v0.12.4/64%20bit/libwkhtmltox.so

EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY [".", "EvenCart/"]
RUN dotnet restore "EvenCart/EvenCart.csproj"

WORKDIR "/src/EvenCart"
RUN dotnet build "EvenCart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EvenCart.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS final



WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvenCart.dll"]
