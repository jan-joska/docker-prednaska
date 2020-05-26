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

### Container

Spuštěná instance Docker image s vlastní sadou parametrů (pokud jsou vyžadovány). Získává automaticky tyto vlastnosti:
- Standardizace - poběží kdekoliv, kde je podpora pro docker
- Malá velikost - umí sdílet součásti (ve kterých se neliší) s jinými konterjnery
- Izolovaný - Nesdílí žadné pro něj specifické prostředky
 

 






