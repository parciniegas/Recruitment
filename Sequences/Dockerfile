#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sequences/Sequences.csproj", "Sequences/"]
RUN dotnet restore "Sequences/Sequences.csproj"
COPY . .
WORKDIR "/src/Sequences"
RUN dotnet build "Sequences.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sequences.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sequences.dll"]