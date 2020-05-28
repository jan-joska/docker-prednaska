# Příklad 2 - Rozběhnutí .net core aplikace z disku hostitelského počítače

Tato ukázka má za cíl předvést spuštění webového api v minimalistickém webovém serveru umístěném v kontejneru.
Aplikace bude překopírována z filesystému hostu do kontejneru. 

K vytvoření aplikace byla použita šablona `webapi` z .net tooling aplikace `dotnet`

```
mkdir c:\temp\docker-prednaska\DemoApi
dotnet new webapi
```

Kroky k rozběhnutí aplikace v kontejneru

1. Stáhnutí image vhodného pro provoz ASP.NET Core 3.1 aplikace
2. Vytvoření souboru dockerfile pro nový image odvozený od bázového image
3. Spuštění kontejneru 

## Stáhnutí image 

Pro naše účely vyhledáme na [docker hubu vhodný image](https://hub.docker.com/_/microsoft-dotnet-core-aspnet)

Vhodný pro naše účel je `mcr.microsoft.com/dotnet/core/aspnet:3.1`

Image stáhneme pomocí příkazu [pull](https://docs.docker.com/engine/reference/commandline/pull/)
docker pull mcr.microsoft.com/dotnet/core/aspnet:3.1
