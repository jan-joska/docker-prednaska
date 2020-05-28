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

```
CONTAINER ID        IMAGE                                        COMMAND                  CREATED             STATUS              PORTS                    NAMES
843ba189b0f0        mcr.microsoft.com/mssql/server:2017-latest   "/opt/mssql/bin/nonr…"   5 minutes ago       Up 5 minutes        0.0.0.0:1450->1433/tcp   sharp_franklin
81fd9be6a8c1        mcr.microsoft.com/mssql/server:2017-latest   "/opt/mssql/bin/nonr…"   18 minutes ago      Up 17 minutes       1433/tcp                 cranky_shaw
```

Mohu se připojit `STD_IN`, `STD_OUT` a `STD_ERR` do konzole příkazem `docker attach sharp_franklin, ale sql server nic nevypisuje

Mohu na běžícím kontejneru spusit příkazovou řádku a připojit interaktivní konzoli příkazem `docker exec -it sharp_franklin /bin/bash`

```
C:\Program Files (x86)\Microsoft Visual Studio\2019\Community>docker exec -it sharp_franklin /bin/bash
root@843ba189b0f0:/#
```



