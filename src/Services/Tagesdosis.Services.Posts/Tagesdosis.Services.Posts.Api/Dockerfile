FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/Tagesdosis.Services.Posts.Api.csproj", "Tagesdosis.Services.Posts.Api/"]
RUN dotnet restore "src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/Tagesdosis.Services.Posts.Api.csproj"
COPY . .
WORKDIR "/src/Tagesdosis.Services.Posts.Api"
RUN dotnet build "Tagesdosis.Services.Posts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Services.Posts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Services.Posts.Api.dll"]
