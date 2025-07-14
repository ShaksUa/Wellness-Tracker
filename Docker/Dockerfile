FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["WellnessTracker.csproj", "./"]
RUN dotnet restore "WellnessTracker.csproj"
COPY . .
RUN dotnet build "WellnessTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WellnessTracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "WellnessTracker.dll"]
