#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Addresses.Service/Addresses.Service.csproj", "Addresses.Service/"]
COPY ["Addresses.Infrastructure/Addresses.Infrastructure.csproj", "Addresses.Infrastructure/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]
COPY ["Addresses.Application/Addresses.Application.csproj", "Addresses.Application/"]
COPY ["Addresses.Domain/Addresses.Domain.csproj", "Addresses.Domain/"]
RUN dotnet restore "Addresses.Service/Addresses.Service.csproj"
COPY . .
WORKDIR "/src/Addresses.Service"
RUN dotnet build "Addresses.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Addresses.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Addresses.Service.dll"]
