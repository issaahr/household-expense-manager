FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/HouseholdExpenseManager.Api.csproj", "./"]
RUN dotnet restore "HouseholdExpenseManager.Api.csproj"
COPY ["src/", "./"]
RUN dotnet publish "HouseholdExpenseManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "HouseholdExpenseManager.Api.dll"]
