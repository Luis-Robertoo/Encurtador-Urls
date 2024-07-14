FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ARG CONNECTION

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG CONNECTION

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Encurtei.API.dll"]