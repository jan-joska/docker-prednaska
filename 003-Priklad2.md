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
3. Vytvoření image
4. Spuštění kontejneru 

## Stáhnutí image 

Pro naše účely vyhledáme na [docker hubu vhodný image](https://hub.docker.com/_/microsoft-dotnet-core-aspnet)

Vhodný pro naše účel je `mcr.microsoft.com/dotnet/core/aspnet:3.1`

Image stáhneme pomocí příkazu [pull](https://docs.docker.com/engine/reference/commandline/pull/)
```
docker pull mcr.microsoft.com/dotnet/core/aspnet:3.1
```
## 2. Vytvoření souboru dockerfile

V rootu aplikace vytvořím soubor s názvem `dockerfile` s tímto obsahem:

```dockerfile
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 # Z tohoto bázového image
WORKDIR /app # Změn adresář v konterjneru / vytvoř jestli neexistuje
COPY /bin/Debug/netcoreapp3.1/ /app # nakopíruj z hostu adresář do adresáře v kontejneru
EXPOSE 80 # zpřístupni port 80 
ENTRYPOINT ["dotnet", "DemoApi.dll"] # jako vstupní bod hlavního procesu spusť tento příkaz
```
## 3. Vytvoření image 

v adresáři se souborem `dockerfile` spustíme příkaz:

```
docker build -t demoapi:ver1 .
```

Vytvořenému image jsme zvolili název `demoapi` a tag `ver1`

Ověříme, že je image správně vytvořen:

```
docker image ls
```
## 4. Spuštění kontejneru

Pro kontejner si přeje následující vlastnosti:
- Nechť je nový kontejner vytvořen na základe image `demoapi:ver1`
- Nechť je nový kontejner pojmenován `demoapi1`
- Nechť je port `80` kontejneru mapován na port `18000` hostitelského počítače
- Nechť je environmentální proměnná s názvem `DEMO_API_VERSION` v kontejneru nastavena na hodnotu `1`

```
docker run --name "demoapi1" -p:18000:80 -e "DEMO_API_VERSION=1" demoapi:ver1
```
Stiskneme kombinaci `CTRL-C`, čímž ukončme vypis terminálu, ale neukončíme kontejner. Ten byl vytvořen v detached režimu.

Ověříme, že kontejner běží
```
docker container ls
```

vidíme že: 
```
:\temp\docker-prednaska\DemoApi>docker container ls
CONTAINER ID        IMAGE               COMMAND                CREATED             STATUS              PORTS                   NAMES
3e646a66c8aa        demoapi:ver1        "dotnet DemoApi.dll"   24 seconds ago      Up 23 seconds       0.0.0.0:18000->80/tcp   demoapi1
```

Zadáme URL do prohlížeče 

```http://localhost:18000/functions/getversion``` 

A vidíme, že environmentální proměnná byla správně nastavena

```
{"version":"1"}
```




