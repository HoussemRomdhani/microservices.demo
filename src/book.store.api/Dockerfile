#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/book.store.api/book.store.api.csproj", "src/book.store.api/"]
RUN dotnet restore "src/book.store.api/book.store.api.csproj"
COPY . .
WORKDIR "/src/src/book.store.api"
RUN dotnet build "book.store.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "book.store.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "book.store.api.dll"]
