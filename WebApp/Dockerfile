FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["RazorApp/RazorApp.csproj", "RazorApp/"]
RUN dotnet restore "RazorApp/RazorApp.csproj"
COPY . .
WORKDIR "/src/RazorApp"
RUN dotnet build "RazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RazorApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RazorApp.dll"]
