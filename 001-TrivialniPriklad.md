Skip to content
Search or jump to…

Pull requests
Issues
Marketplace
Explore
 
@jan-joska 
jan-joska
/
docker-prednaska
1
00
 Code
 Issues 0
 Pull requests 0 Actions
 Projects 0
 Wiki
 Security 0
 Insights
 Settings
docker-prednaska/001-TrivialniPriklad
@jan-joska jan-joska Create 001-TrivialniPriklad
516d0fd 10 seconds ago
35 lines (23 sloc)  918 Bytes
  
# Triviální příklad funkce dockeru

Provedeme: 

1. Stáhnutí obrazu speciálně připraveného miniaturního linuxu `alpine`
2. Spuštění kontejneru v interaktivním režimu
3. Vykonáme příkazy v izolovaném kontejneru
4. Ukončení kontejneru
5. Opětovné nastartování konterjneru
6. Odmazání konterjneru

### Stažení image/obrazu

Stažení provedeme příkazem `pull` jedná se o zkratku plného příkazu `docker image pull`

```
docker pull alpine:latest
```
Ověříme, že je image k dispozici. Zkratka `ls` představuje list.

```
docker image ls 
```

### Spustíme nový kontejner

Pro spuštění konterjneru se používá příkaz `docker run`. Přepínač `-i` a `-t` způsobí navázání standardních streamů a alokaci terminálového okna.

Přepínač `--name` nám umožňuje kontejner pojmenovat a tím stadněji nalézt.

```
docker run -it --name "alpine1" alpine:latest
```


© 2020 GitHub, Inc.
Terms
Privacy
Security
Status
Help
Contact GitHub
Pricing
API
Training
Blog
About
