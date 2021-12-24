# Planet Wars
Studentski projekat iz Arhitekture i projektovanja softvera.
## Autori:
* [welcomepeople](https://github.com/welcomepeople)
* [cats9rule](https://github.com/cats9rule)

## Tehnologije:
- Frontend: Angular
- Message Broker: SignalR
- Backend: .NET Core
- DB: MS SQL

## Pravila igre

Igrač na početku poteza dobija određeni broj armija. Na samom početku igre, svaki igrač dobija po 3 armije, a u svakom sledećem potezu broj armija zavisi od broja planeta koje taj igrač poseduje. Igrač u okviru poteza ima obavezu da dobijene armije rasporedi, i da odigra maksimalno dve akcije. Akcije mogu biti pomeranje određenog broja armija sa jedne planete na susednu, i napad neke planete koju poseduje neki drugi igrač. Igrač ima pravo da izabere željenu kombinaciju akcija. Neke planete mogu imati određene resurse, koji utiču na snagu armija koje napadaju s te planete, snagu armija koje brane planetu, i razdaljinu koju armije s te planete mogu da pređu, a koja je podrazumevano do susedne planete. 

Napad se odvija tako što igrač izabere planetu sa koje napada, izabere broj armija kojim napada, i koju planetu će napasti. Drugi igrač se brani pasivno, i ako je njegova odbrana veća od moći napada prvog igrača, pobeđuje. Pri napadanju se gubi onaj broj armija koliko je potrebno (na primer, ako prvi igrač napada sa 3 armije, a drugi se brani sa 2, prvi će pobetiti ali će mu ostati samo jedna armija da zauzme planetu). 

Zauzimanje planeta se dešava u dva slučaja: prvi ako planeta nema vlasnika, što je situacija na početku igre, gde je dovoljno samo pomeriti armiju na datu planetu, i drugi, kada se planeta zauzima napadom i u tom slučaju će biti preoteta samo ako napadaču pretekne neka armija iz napada. U suprotnom, planeta ostaje u vlasništvu drugog igrača. Ako se napad vrši na planetu koja je zauzeta ali nema armije, napadač ne gubi nijednu armiju i samo zauzima planetu.


## Kraj igre
Kraj igre je onda kada svi sem jednog igrača ostanu bez planeta.
