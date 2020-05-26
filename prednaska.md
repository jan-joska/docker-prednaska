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

### Container

Spuštěná instance Docker image s vlastní sadou parametrů (pokud jsou vyžadovány). Získává automaticky tyto vlastnosti:
- Standardizace - poběží kdekoliv, kde je podpora pro docker
- Malá velikost - umí sdílet součásti (ve kterých se neliší) s jinými konterjnery
- Izolovaný - Nesdílí žadné pro něj specifické prostředky

Spouštění konterjnerů zabere řáově sekundy nikoliv minuty jako u virtuálních strojů.
Kontejner se stará o jeden spuštěný proces. Představuje spuštěnou úlohu. Poté co hlavní proces zaniká, zaniká i konterjner. Může to být spuštění jednoho příkazu v příkazové řádce i spuštění webového serveru po dlouhou dobu. Jakákoliv data ukládaná do virtuálního filesystému jsou při zániku kontejneru ztracena.
 
###  Volumes

Kdyby kontejner neměl schopnost interagovat s hostitelským systémem nebyl by asi moc užitčený. Proto je nutné určité informace kontrolovaným způsobem vyměňovat. Volumes je prostředek jak umožnit přístup kontejnerů k filesystému hostovacího stroje. 

Při ukládání dat do volumes, tyto data přežijí zánik kontejneru.

### Networks

Mezi kontejnery nebo jejich podmnožinami lze vytvářet virtuální sítě. Řídí se tím, jaké konterjery mohou spolu komunikovat.

### Image repository

Je úložiště pro docker image. Může být veřejné jako [Docker hub)(https://hub.docker.com/), nebo privátní.
Pro nás je repository primární zdroj základních image pro vytváření našich vlastních konterjnerů
Do repository mohu umisťovat vytvořené image.

### Docker file 

Je soubor s instrukcemi pro vytvoření image. Obsahuje sadu očekávatelných příkazů pro přípravu prostředí pro kontejner. Uzancí je název .dockerfile

Né každý stažený image má k dispozici dockerfile.

### Instalace Dockeru do Windows

- Musíme mít Windows 10 Professional s podporou HyperV
- Stáhneme a nainstalujeme [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows/)

# Příklad 1 - Stáhnutí a rozběhnutí SQL Serveru

1. Prohledáme [http://hub.docker.com](http://hub.docker.com) a vyhledáme oficiální [image SQL Serveru](https://hub.docker.com/_/microsoft-mssql-server)
2. Zvolíme si `2017-latest`
3. Spustíme příkaz `docker pull mcr.microsoft.com/mssql/server:2017-latest`
4. Po stáhnutí si ověříme jaké všechny image máme v lokálním úložišti `docker image ls`
5. Nebude vytvářet náš vlastní image odvozený od stáhnutého, ale rovnou vytvoříme ad hoc vytvořený kontejner
6. Spustíme `docker run --rm -p 1450:1433 -e ACCEPT_EULA=Y -e SA_PASSWORD=ComplexPassword123 mcr.microsoft.com/mssql/server:2017-latest` 

Rozpad příkazu 

<dl>
  <dt>docker run</dt>
  <dd>CLI příkaz pro rozběhnutí kontejneru založeném na image</dd>
  <dt>--rm</dt>
  <dd>odstranit kontejner po doběhnutí</dd>
 <dt>-p 1450:1433</dt>
  <dd>Odkrýt (expose) port zevnitř kontejneru do hostovacího stroje. Formát PORT_NA_HOSTU:PORT_V_KONTEJNERU </dd>
 <dt>-e </dt>
  <dd>Nastavit environmentální proměnnou - tímto způsobem probíhá předávání učitých startup proměnných do instance image</dd>
 <dt>mcr.microsoft.com/mssql/server:2017-latest</dt>
  <dd> Název image mcr.microsoft.com/mssql/server včetně tagu 2017-latest</dd>
</dl>

Na SQL server se lze nyní připojit např. pomocí SQL Management studio takto `localhost, 1450`

Po opuštění konzole pomocí `CTRL-C` kontejner pokračuje v činnosti dokud není nějakým způsobem zastaven jeho hlavní proces.

Ověříme, že stále běží pomocí `docker container ls`

Ověříme jestli stále běží pomocí `docker container ls`
