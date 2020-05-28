# Příklad 1 - Stáhnutí a rozběhnutí SQL Serveru

Tato ukázka má za cíl předvést spuštění SQL Serveru 2017 v konterjneru.

Kroky k rozběhnutí SQL Serveru 2017 v kontejneru: 

1. Stažení image s požadovanou verzí SQL Serveru
2. Spuštění nového kontejneru s příslušnou konfigurací

## 1. Stažení mage

- Prohledáme [http://hub.docker.com](http://hub.docker.com) a vyhledáme oficiální [image SQL Serveru](https://hub.docker.com/_/microsoft-mssql-server)
- Zvolíme si `2017-latest`
- Spustíme příkaz `docker pull mcr.microsoft.com/mssql/server:2017-latest`
- Po stáhnutí si ověříme jaké všechny image máme v lokálním úložišti `docker image ls`

## 2. Spuštění kontejneru

- Nebude vytvářet náš vlastní image odvozený od stáhnutého, ale rovnou spustíme kontejnera na základě bázového image
- Spustíme: 

```batchfile
docker run --name "sqlserver2017" -d -p 1450:1433 -e ACCEPT_EULA=Y -e SA_PASSWORD=ComplexPassword123 mcr.microsoft.com/mssql/server:2017-latest
``` 

Rozpad příkazu 

<dl>
  <dt>[docker run](https://docs.docker.com/engine/reference/run/)</dt>
  <dd>CLI příkaz pro rozběhnutí kontejneru založeném na image</dd>
  <dt>-d</dt>
  <dd>Vytvořit v detached stavu - na pozadí. Neuvidíme `std_out`</dd>
 <dt>-p 1450:1433</dt>
  <dd>Odkrýt (expose) port zevnitř kontejneru do hostovacího stroje. Formát *PORT_NA_HOSTU*:PORT_V_KONTEJNERU </dd>
 <dt>-e </dt>
  <dd>Nastavit environmentální proměnnou - tímto způsobem probíhá předávání učitých startup proměnných do instance image</dd>
 <dt>mcr.microsoft.com/mssql/server:2017-latest</dt>
  <dd> Název image mcr.microsoft.com/mssql/server včetně tagu 2017-latest</dd>
</dl>

Na SQL server se lze nyní připojit např. pomocí SQL Management studio takto `localhost, 1450`

Ověříme, že stále běží pomocí `docker container ls`



