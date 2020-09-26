#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApplication15/WebApplication15.csproj", "WebApplication15/"]
RUN dotnet restore "WebApplication15/WebApplication15.csproj"
COPY . .
WORKDIR "/src/WebApplication15"
RUN dotnet build "WebApplication15.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication15.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:80
ENTRYPOINT ["dotnet", "WebApplication15.dll"]