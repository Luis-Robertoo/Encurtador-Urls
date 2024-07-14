FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ENV CONNECTION={CONNECTION}
ENV CARALHO=DEASA
 
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG CONNECTION
ENV TESTE=OLA
ENV CONNECTION=$CONNECTION
ENV BOSTA=CAGADA

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Encurtei.API.dll"]