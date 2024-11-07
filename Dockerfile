FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ARG CONNECTION
ARG ROUTE_API

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG CONNECTION
ARG ROUTE_API

WORKDIR /App
COPY --from=build-env /App/out .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "Encurtei.API.dll"]
