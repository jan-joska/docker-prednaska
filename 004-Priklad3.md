# Příklad 3 - Multistage build

K vytvoření image je někdy zapotřebí použít jiných konterjnerů s různými schopnostmi. Příklad:

Pro překlad a publish aplikace napsané v `.net core 3.1` potřebuji mít stroj s nainstalovaným `SDK` - využiji tedy image `mcr.microsoft.com/dotnet/core/sdk:3.1-buster`

Pro spuštění aplikace ale žádné SDK nepotřebuji a přítomné nástroje by zvětšovaly velikost výsledného zájmového image. Pro spuštění aplikace bohatě postačí přítomnost `.net core 3.1 Runtime` a tedy image `mcr.microsoft.com/dotnet/core/runtime:3.1`

Docker podporuje tzv. multistage build (vícefázový build), kdy utilitární image potřebné pro vytvoření finálního vznikají a zanikají dle potřeby. Mezi konterjnery lze jednoduše pomocí příkazů kopírovat výsledky jejich práce.

Příklad: 

```dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src/ConsoleApp
COPY ["*.csproj", ""]

RUN dotnet restore "ConsoleApp.csproj"
COPY . .

RUN dotnet build "ConsoleApp.csproj" -c Release

WORKDIR /src/ConsoleApp/build

FROM build AS publish
WORKDIR /src/ConsoleApp
RUN dotnet publish "ConsoleApp.csproj" -c Release -o /app/ConsoleApp

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 as final
WORKDIR /app/ConsoleApp
COPY <b>--from=publish</b> /app/ConsoleApp .
ENTRYPOINT ["dotnet", "ConsoleApp.dll"]
```


