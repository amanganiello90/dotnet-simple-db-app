FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /app

## copy csproj and app with build
COPY . ./dotnetapp/
WORKDIR /app/dotnetapp
RUN dotnet publish -c Release -o out

## If you want console color
#FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime  

## Runtime console app
#FROM mcr.microsoft.com/dotnet/core/runtime:2.2-alpine AS runtime

## If you want WEB APP library
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app/dotnetapp/out ./
EXPOSE 5001
ENTRYPOINT ["dotnet", "dotnetapp.dll"]