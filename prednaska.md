# Přednáška DOCKER

1. Co je Docker
2. Jaké problémy Docker řeší
3. Z čeho se Docker skládá
## Co je Docker

Vyvíjení software dnes klade požadavky na využívání různorodých technologií, jazyků, frameworků, stále se proměňujících API apod.

Jako příklad můžeme uvést aplikaci se složitější vnitřní funkcí. V naší běžné praxi využíváme tyto technologie:
- [ASP.Net Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1)
- [SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017)
- Oracle server
- [RabbitMq](https://www.rabbitmq.com/) (Message broker)
- Aplikace běžící ve formě služby
- Klienty pro interakce se systémy SAP

Takové množství závislostí představuje nemalou komplexitu řešení. Je nutné na jednom vývojářském stroji mít i několik 
instancí SQL Serveru, aby byla zachována schopnost používat starší databáze.

Některé projekty mohou vyžadovat specifickou verzi závislosti a na vývojářském stroji nelze mít několik verzí knihoven zároveň. 

S instalací všemožných služeb je spojena časová režie.

V minulosti byly obdobné úlohy řešeny za pomocí virtualizace. Byl vytvořen virtuální stroj, který simuloval veškerý hardware a operační systém. Takové stroje bylo možné zapínat a vypínat dle potřeby. Nutnost instalace a přípravy prostředí, ale byla stále zachována.

# Přichází kontejnery

Předobrazem termínu kontejner je standardizovaný přepravní lodní kontejner. Je to jednotka, kterou je možné přepravovat mnoha druhy dopravních prostředku a tvoří samostatnou izolovanou jednotku.

Kontejner je jednotka software, která zaobaluje kód a všechny jeho závislosti tak, že aplikace může spolehlivě běžet v různých prostředích nezávisle. Kontejner obsahuje vše, co je potřeba k rozběhnutí aplikace: kód, běhové prosředí (runtime), systémové nástroje, knihovny a nastavení.

Narozdíl od virtuálů, kontejnery dokážou sdílet shodné části, které ke své činnosti potřebují, ale jsou dokonale izolovány jeden od druhého. 

![Rozdíl virtuální stroj a kontejner](https://github.com/jan-joska/docker-prednaska/blob/master/Images/virtual-vs-container.png)

# Z čeho se skládá Docker

![Součásti Docker](https://github.com/jan-joska/docker-prednaska/blob/master/Images/engine-components-flow.png)

Řešení Docker pro kontejnerizaci aplikací se skládá z těchto částí: 

### Docker Host

Je fyzický počítač nebo virtuální stroj využívající Linux nebo Widnows. Může to být laptop, server, virtuální stroj v datacentru nebo zdroj v cloudu. Na tomto hostu-hostiteli běží Docker daemon

### Docker daemon - server

Docker daemon je služba běžící na hostovacím operačním systému. Svoje funkce vystavuje pomocí REST API. Původně služba musela běžet pouze na operačním systému Linux, protože využívala některé jeho funkce. Ale nyní je k dispozici i pro Windows a MacOS.

### Docker CLI (Command-line interface)

[Docker CLI](https://docs.docker.com/engine/reference/commandline/cli/) je klient sloužící k ovládání Docker služby.

### Docker image

Docker image je read-only šablona, která obsahuje monžinu instrukcí pro vytvoření kontejneru, který může běžet na Docker platformě.
Dal by se představit jako instrukce k vytvoření kontejneru za pomocí příkazů, kde každý z nich vytvoří novou vrstvu. 
Hrubě by se dalo přirovnat k třídě z které jsou vytvářeny instance-kontejnery.

[Název image](https://cloud.google.com/artifact-registry/docs/docker/names) se skládá ze základní části a jednoho nebo několiga tagů. Jestliže tag není uveden využije se výchozí tag latest. 

#### Příklad názvu image v úložišti ####
`mcr.microsoft.com/mssql/server:2019-latest`

#### Příklad tagů ####

```
2017-latest
2019-latest
2019-CU4-ubuntu-16.04
2019-GDR1-ubuntu-16.04
2017-CU8-ubuntu
```

### Container

Spuštěná instance Docker image s vlastní sadou parametrů (pokud jsou vyžadovány). Získává automaticky tyto vlastnosti:
- Standardizace - poběží kdekoliv, kde je podpora pro docker
- Malá velikost - umí sdílet součásti (ve kterých se neliší) s jinými konterjnery
- Izolovaný - Nesdílí žadné pro něj specifické prostředky

Spouštění konterjnerů zabere řáově sekundy nikoliv minuty jako u virtuálních strojů.
Kontejner se stará o jeden spuštěný proces. Představuje spuštěnou úlohu. Poté co hlavní proces zaniká, zaniká i konterjner. Může to být spuštění jednoho příkazu v příkazové řádce i spuštění webového serveru po dlouhou dobu. Jakákoliv data ukládaná do virtuálního filesystému jsou při zániku kontejneru ztracena.
 
###  Volumes

Kdyby kontejner neměl schopnost interagovat s hostitelským systémem nebyl by asi moc užitčený. Všechna změněná data jsou zapsána ve filesystému kontejneru a zanikají společně s ním.

Pokud si přejeme data zachovat, nebo sdílet mezi kontejnery využíváme tzv. volumes. Je to abstrakce adresáře, který je naveden buď na náhodně vytvořený adresář v hostitelském systému, anbo do konkrétního adresáře. Může být pojmenovaný nebo opatřen náhodným jménem. 

### Networks

Mezi kontejnery nebo jejich podmnožinami lze vytvářet virtuální sítě. Řídí se tím, jaké konterjery mohou spolu komunikovat.

### Image repository

Je úložiště pro docker image. Může být veřejné jako [Docker hub)(https://hub.docker.com/), nebo privátní.
Pro nás je repository primární zdroj základních image pro vytváření našich vlastních konterjnerů
Do repository mohu umisťovat vytvořené image.

### Vrstvy docker

Díky principu znovupoužitelnosti vrstev v dockeru dochází k účelnému znovuužívání již vytvořených images. Každý příkaz vykonaný v kontejneru překopírovává ovlivněné soubory do nesvrchnější vrstvý, která jediná je Read/Write. Všechny vrstvy pod ní jsou read-only kopie souboru z vrstvy pod nimi.

![Vrstvy docker](https://github.com/jan-joska/docker-prednaska/blob/master/Images/layers.PNG)


### Docker file 

Je soubor s instrukcemi pro vytvoření image. Obsahuje sadu očekávatelných příkazů pro přípravu prostředí pro kontejner. Uzancí je název .dockerfile

Né každý stažený image má k dispozici dockerfile.






