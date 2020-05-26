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

![Rozdíl virtuální stroj a kontejner](https://github.com/jan-joska/docker-prednaska/blob/master/Images/container-overview-layers.png)


 






