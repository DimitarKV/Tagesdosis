FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gateways/Tagesdosis.Gateways.Portal/Tagesdosis.Gateways.Portal.csproj", "Tagesdosis.Gateways.Portal/"]
RUN dotnet restore "src/Gateways/Tagesdosis.Gateways.Portal/Tagesdosis.Gateways.Portal.csproj"
COPY . .
WORKDIR "/src/Tagesdosis.Gateways.Portal"
RUN dotnet build "Tagesdosis.Gateways.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Gateways.Portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Gateways.Portal.dll"]
