FROM mcr.microsoft.com/dotnet/sdk:8.0.203-alpine3.19-amd64 AS build
WORKDIR /src
COPY ./CompanyName.DDD.API ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-alpine3.19-amd64
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=America/Lima
RUN apk update && apk add libgdiplus icu-libs libc6-compat tzdata
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT="Local"

WORKDIR /app
RUN mkdir Documents
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CompanyName.DDD.API.dll"]