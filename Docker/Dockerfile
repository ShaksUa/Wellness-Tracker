FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Wellness Tracker.csproj", "./"]
RUN dotnet restore "Wellness Tracker.csproj"
COPY . .
RUN dotnet build "Wellness Tracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wellness Tracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Wellness Tracker.dll"]
