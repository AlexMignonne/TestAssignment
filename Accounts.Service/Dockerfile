#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Accounts.Service/Accounts.Service.csproj", "Accounts.Service/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]
COPY ["Accounts.Infrastructure/Accounts.Infrastructure.csproj", "Accounts.Infrastructure/"]
COPY ["Accounts.Domain/Accounts.Domain.csproj", "Accounts.Domain/"]
COPY ["Accounts.Application/Accounts.Application.csproj", "Accounts.Application/"]
RUN dotnet restore "Accounts.Service/Accounts.Service.csproj"
COPY . .
WORKDIR "/src/Accounts.Service"
RUN dotnet build "Accounts.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Service.dll"]
