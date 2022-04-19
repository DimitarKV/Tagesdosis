FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Api/Tagesdosis.Services.User.Api.csproj", "Tagesdosis.Services.User.Api/"]
RUN dotnet restore "src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Api/Tagesdosis.Services.User.Api.csproj"
COPY . .
WORKDIR "/src/Tagesdosis.Services.User.Api"
RUN dotnet build "Tagesdosis.Services.User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Services.User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Services.User.Api.dll"]
