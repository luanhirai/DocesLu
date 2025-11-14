FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

# Copia o arquivo do projeto e restaura dependências
COPY ./DocesLu.csproj ./
RUN dotnet restore DocesLu.csproj

# Copia o restante dos arquivos e faz o build
COPY . ./
COPY ./Storage ./Storage
RUN dotnet publish DocesLu.csproj -c Release -o out

# Imagem final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "DocesLu.dll"]
