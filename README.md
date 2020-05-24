# ForumBXS

## Services
1. Registration of questions
2. Registration of answers

## Pattners
> Simple -> DDD, CQRS, Mediator

## Requirements

> .Net Core 3.1 -> https://dotnet.microsoft.com/download/dotnet-core/3.1

> Docker -> https://docs.docker.com/desktop/

## Local tests (docker)

> docker build -f Dockerfile -t forumbxs:latest .

> docker run --rm -d -p 3001:80 forumbxs

## Local tests (dotnet)

> dotnet restore

> dotnet publish -c Release -o out

> dotnet out/ForumBXS.WebAPI.dll

## Swagger / Open API

> {{url}}/swagger

## Local automated tests

> dotnet test -v n