FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Grpc/Tagesdosis.Services.User.Grpc.csproj", "Tagesdosis.Services.User.Grpc/"]
RUN dotnet restore "src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Grpc/Tagesdosis.Services.User.Grpc.csproj"
COPY . .
WORKDIR "/src/Tagesdosis.Services.User.Grpc"
RUN dotnet build "Tagesdosis.Services.User.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagesdosis.Services.User.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagesdosis.Services.User.Grpc.dll"]
