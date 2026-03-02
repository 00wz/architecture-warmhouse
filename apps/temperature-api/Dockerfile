FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TemperatureApi.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 8081
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8081
ENTRYPOINT ["dotnet", "TemperatureApi.dll"]