# Imagem base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

# Copia os arquivos
COPY ./ /app

# Testes automatizados
WORKDIR /app/test
RUN dotnet test "ForumBXS.DomainTest/ForumBXS.DomainTest.csproj" 

# Web api
WORKDIR /app/src/ForumBXS.WebAPI
RUN dotnet publish -c Release -o out

# Imagem base final
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# Pasta de trabalho e onde ficarão as dlls
WORKDIR /app/dll

# Arquivos da compilacao
COPY --from=build-env /app/src/ForumBXS.WebAPI/out /app/dll

# Variaveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Development
ENV FORUMBXS_DATABASE_NAME=forum-bxs

# Sustenta o container
CMD ["dotnet", "ForumBXS.WebAPI.dll"]