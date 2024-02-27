FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build

WORKDIR /src
COPY ["Poc.Apryse.csproj", "."]

RUN dotnet restore "./Poc.Apryse.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Poc.Apryse.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poc.Apryse.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bookworm-slim AS base

RUN apt-get update

RUN apt-get install -y libnss3-dev

RUN apt-get install -y fontconfig

RUN apt-get install -y libglib2.0

RUN apt-get install -y libatk1.0-0

RUN apt-get install -y libatspi2.0-0

RUN apt-get install -y libxcomposite1

RUN apt-get install -y libgbm-dev

RUN apt-get install -y libdrm-amdgpu1

RUN apt-get install -y libxkbcommon-dev

RUN apt-get install -y libxrandr-dev

RUN apt-get install -y libxdamage-dev

RUN apt-get install -y libxfixes-dev

WORKDIR /app

# Copy the HTML2PDFLinux.tar.gz archive into the container
COPY ./HTML2PDFLinux.tar.gz .

# Unzip the archive into the /app directory
RUN tar -xzvf HTML2PDFLinux.tar.gz -C /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
RUN useradd nonroot -m
USER nonroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.Apryse.dll"]


FROM base AS final