# Use imagem oficial do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura dependências
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Porta padrão do ASP.NET
EXPOSE 80

ENTRYPOINT ["dotnet", "NotiomSimples.dll"]