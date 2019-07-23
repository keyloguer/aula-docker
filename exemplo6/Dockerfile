FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build-env
WORKDIR /app
# COPIAR A O ARQUIVO .sln e os .csproj 
COPY Hackathon.sln .
COPY src/HackatonBtp.WebApi/*.csproj ./src/HackatonBtp.WebApi/
COPY src/HackathonBtp.Domain/*.csproj ./src/HackathonBtp.Domain/
COPY src/HackathonBtp.Data/*.csproj ./src/HackathonBtp.Data/
COPY src/HackathonBtp.Application/*.csproj ./src/HackathonBtp.Application/
COPY src/HackathonBtp.AzureFunctions/*.csproj ./src/HackathonBtp.AzureFunctions/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/src/HackathonBtp.Application/out/ ./HackathonBtp.Application/out/
COPY --from=build-env /app/src/HackathonBtp.Data/out/ ./HackathonBtp.Data/out/
COPY --from=build-env /app/src/HackathonBtp.Domain/out/ ./HackathonBtp.Domain/out/
COPY --from=build-env /app/src/HackathonBtp.AzureFunctions/out/ ./HackathonBtp.AzureFunctions/out/
COPY --from=build-env /app/src/HackatonBtp.WebApi/out/ ./HackatonBtp.WebApi/out/
COPY app.sh .
CMD /bin/bash /app/app.sh