# 🧪 ParlemTest - Prova Tècnica

Aquesta solució correspon a la part **back-end** de la prova tècnica proposada per Parlem. L’objectiu principal ha estat desenvolupar una **Web API** seguint bones pràctiques de desenvolupament, arquitectura neta i separació de responsabilitats.


## 🔧 Tecnologies Utilitzades

- **.NET 8 (ASP.NET Core)** com a framework principal.
- **MongoDB** com a base de dades NoSQL.
- **Clean Architecture** per una estructura de codi desacoblada i mantenible.
- **AutoMapper** per al mapatge entre entitats i DTOs.
- **xUnit** per a tests unitaris.
  

  ## 🧠 Principis Aplicats

- **Arquitectura Neta (Clean Architecture)**: Separació clara entre presentació, aplicació, domini i infraestructura.
- **Injecció de dependències**: Serveis i repositoris injectats per facilitar proves i escalabilitat.
- **Mapeig DTO/Entitats**: Ús d'AutoMapper per mantenir desacoblats els models de domini de les dades exposades.
- **Persistència amb MongoDB**: S’ha utilitzat una base de dades NoSQL per la seva flexibilitat i simplicitat.
  

## 📌 Consideracions

- El backend està dissenyat per connectar-se fàcilment amb un front-end que mostri una fitxa de client amb els seus productes contractats.
- L’arquitectura permet escalar fàcilment afegint noves funcionalitats sense modificar el codi existent de forma dràstica.
- La capa d'infraestructura està desacoblada de la lògica de negoci, fet que facilita canviar el proveïdor de dades (per exemple, passar de MongoDB a una base de dades SQL).


## ✅ Com Executar

1. Assegura’t de tenir instal·lat [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Clona el repositori.
3. Executa el projecte `ParlemTest.Api` com a projecte d’inici.
4. Assegura’t que MongoDB estigui en funcionament (pots utilitzar Docker o una instància local).


💬 Per qualsevol dubte o suggeriment, pots obrir un issue o contactar amb mi directament.
