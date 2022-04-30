FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Grpc/Tagesdosis.Services.Posts.Grpc.csproj", "Tagesdosis.Services.Posts.Grpc/"]
RUN dotnet restore "src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Grpc/Tagesdosis.Services.Posts.Grpc.csproj"
COPY . .
WORKDIR "/src/Tagesdosis.Services.Posts.Grpc"
RUN dotnet build "Tagesdosis.Services.Posts.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Services.Posts.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Services.Posts.Grpc.dll"]
