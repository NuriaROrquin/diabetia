#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Diabetia.API/Diabetia.API.csproj", "Diabetia.API/"]
RUN dotnet restore "Diabetia.API/Diabetia.API.csproj"
COPY . .
WORKDIR "/src/Diabetia.API"
RUN dotnet build "Diabetia.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Diabetia.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Diabetia.API.dll"]