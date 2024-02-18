FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env

# Install dotnet debug tools
RUN dotnet tool install --tool-path /tools dotnet-trace \
  && dotnet tool install --tool-path /tools dotnet-counters \
  && dotnet tool install --tool-path /tools dotnet-dump \
  && dotnet tool install --tool-path /tools dotnet-gcdump

WORKDIR /app
COPY . .
RUN dotnet publish "ThreadTest.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

# Copy dotnet-tools
WORKDIR /tools
COPY --from=build-env /tools .

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app

COPY --from=build-env /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ThreadTest.dll"]
