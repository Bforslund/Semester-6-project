#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HotelService/HotelService.csproj", "HotelService/"]
RUN dotnet restore "HotelService/HotelService.csproj"
COPY . .
WORKDIR "/src/HotelService"
RUN dotnet build "HotelService.csproj" -c Release -o /app/build
RUN dotnet test "HotelService.csproj"

FROM build AS publish
RUN dotnet publish "HotelService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelService.dll"]
