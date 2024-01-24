# Technisch Ontwerp Document

## Inleiding

Dit document beschrijft het technische ontwerp voor een Blazor C# API-koppeling met de Blizzard API die gebruik maakt van OAuth.

## Inhoudsopgave

1. [HTTP Request Client](#item-one)
2. [URL Builder](#item-two)
3. [OAuth Authenticatie](#item-three)
4. [Token Ophalen](#item-four)
5. [Endpoints en Parameters](#item-five)
6. [Data normalization](#item-six)
7. [Foutafhandeling](#item-seven)
8. [Conclusie](#item-eight)
<a id="item-one"></a>
### HTTP Request Client

De applicatie zal gebruik maken van een HTTP-client, zoals HttpClient in .NET, die GET-verzoeken verstuurt naar de Blizzard API. De client zal headers instellen voor authenticatie en andere benodigde informatie. De antwoorden van de API worden vervolgens verwerkt en de relevante gegevens worden geëxtraheerd.
<a id="item-two"></a>
### URL Builder
De URL Builder zal een basis-URL hebben voor de Blizzard API. Afhankelijk van de specifieke API-aanroep die nodig is, zal de builder de juiste endpoints en parameters toevoegen aan de basis-URL. Dit zorgt voor flexibiliteit en herbruikbaarheid van code.
<a id="item-three"></a>
### OAuth Authenticatie
De applicatie zal OAuth 2.0 gebruiken voor authenticatie. Dit houdt in dat de applicatie een toegangstoken van de Blizzard API zal aanvragen met behulp van de client-ID en het client-geheim. Dit token wordt vervolgens gebruikt om geautoriseerde aanvragen te doen aan de API.

Voor informatie over Oauth graag kijken naar https://develop.battle.net/documentation/guides/using-oauth
De Authorize URI https://oauth.battle.net/authorize
De Token URI https://oauth.battle.net/token
Voor nu heb je de OAuth credentials nodig voor de volgende Get request:

    GET /oauth/userinfo
    GET /profile/user/wow
    GET /profile/user/wow/protected-character/{realm-id}-{character-id}
    GET /profile/user/wow/collections
    GET /profile/user/wow/collections/pets
    GET /profile/user/wow/collections/mounts

<a id="item-four"></a>
### Token Ophalen

Het ophalen van het token gebeurt tijdens de OAuth-authenticatieflow. Het token wordt opgeslagen en wordt gebruikt voor alle volgende API-aanvragen. Het token verloopt na 24 uur zorg ervoor dat er gechecked wordt of het valid is en dan opnieuw aanvragen mocht dat het geval zijn.
<a id="item-five"></a>
### Endpoints en Parameters
De applicatie zal verschillende endpoints van de Blizzard API gebruiken, afhankelijk van de benodigde gegevens. De benodigde parameters voor elk endpoint worden dynamisch toegevoegd door de URL Builder. De gegevens van de API-respons worden genormaliseerd en opgeslagen in een gestructureerd formaat voor verdere verwerking.


#### Get User
```http
GET /profile/user/wow 
```

| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

#### Get Achievements

```http
GET /profile/wow/character/{realmSlug}/{characterName}/achievements 
```

| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `realmSlug`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `characterName`  | `string` |   :heavy_check_mark:       | The locale to reflect in localized data.  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

#### Get Encounters
```http
GET /profile/wow/character/{realmSlug}/{characterName}/encounters 
```
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `realmSlug`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `characterName`  | `string` |   :heavy_check_mark:       | The locale to reflect in localized data.  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

#### Get Equipment
```http
GET /profile/wow/character/{realmSlug}/{characterName}/equipment 
```
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `realmSlug`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `characterName`  | `string` |   :heavy_check_mark:       | The locale to reflect in localized data.  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |
<a id="item-six"></a>
# Datanormalisatie

Datanormalisatie is een essentieel proces in ons applicatieontwerp dat helpt om de efficiëntie van onze datastructuur te verbeteren en redundantie te verminderen. Dit proces omvat het organiseren van gegevens in objecten of entiteiten om duplicatie te minimaliseren.

## Datastructuur

We hebben twee hoofdentiteiten in onze applicatie: `Player` en `Game`.

### Player Entiteit

De `Player` entiteit slaat informatie op over elke speler. Hier is een voorbeeld van hoe deze entiteit eruit zou kunnen zien:

```csharp
public class Player
{
    public Guid PlayerId { get; set; }
    public string Name { get; set; }
    public Guid GameId { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public string Faction { get; set; }
}
```
### Game Entiteit

De `Game` entiteit slaat informatie op over elk spel. Hier is een voorbeeld van hoe deze entiteit eruit zou kunnen zien:

```csharp
public class Game
{
    public Guid GameId { get; set; }
    public string Name { get; set; }
}
```
## Normalisatieproces

We hebben de principes van datanormalisatie toegepast op deze entiteiten om de gegevensintegriteit te waarborgen en de prestaties te optimaliseren. Specifiek hebben we:

1. Ervoor gezorgd dat elke eigenschap in een entiteit een unieke waarde heeft en dat elke waarde atomair is (1NF).
2. Gegevens gescheiden in verschillende entiteiten op basis van de relaties tussen de eigenschappen (2NF).
3. Eigenschappen verwijderd die niet afhankelijk zijn van de primaire sleutel (3NF).

Door deze principes van datanormalisatie toe te passen, kunnen we een efficiënte, flexibele en betrouwbare datastructuur creëren. Dit zal ons helpen om de prestaties van onze Blazor C# API-integratie met de Blizzard API te optimaliseren.
<a id="item-seven"></a>
### Error handeling

De applicatie zal error's afhandelen die kunnen optreden tijdens het proces, zoals netwerkfouten, API-fouten, enz. Deze fouten worden gelogd voor debugging en de gebruiker wordt op de hoogte gebracht van het probleem op een manier die hun begrijpen. 
<a id="item-eigth"></a>
## Conclusie

Dit technisch ontwerp document biedt een gedetailleerd overzicht van de technische aspecten van de applicatie. Het volgende stadium van dit project zou de implementatie van deze ontwerpen zijn. Bij vragen of opmerkingen kunt u contact opnemen met de auteur van dit document.



