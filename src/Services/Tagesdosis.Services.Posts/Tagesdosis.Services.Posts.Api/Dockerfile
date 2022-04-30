FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/Tagesdosis.Services.Posts.Api.csproj", "src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/"]
COPY ["src/Common/Tagesdosis.Infrastructure/Tagesdosis.Infrastructure.csproj", "src/Common/Tagesdosis.Infrastructure/"]
COPY ["src/Common/Tagesdosis.Domain/Tagesdosis.Domain.csproj", "src/Common/Tagesdosis.Domain/"]
COPY ["src/Common/Tagesdosis.Application/Tagesdosis.Application.csproj", "src/Common/Tagesdosis.Application/"]
COPY ["src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.csproj", "src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts/"]
RUN dotnet restore "src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/Tagesdosis.Services.Posts.Api.csproj"
COPY . .
WORKDIR "/src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api"
RUN dotnet build "Tagesdosis.Services.Posts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Services.Posts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Services.Posts.Api.dll"]
