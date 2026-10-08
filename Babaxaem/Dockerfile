FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Babaxaem.csproj", "."]
RUN dotnet restore "Babaxaem.csproj"

COPY . .
RUN dotnet publish "Babaxaem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Babaxaem.dll"]