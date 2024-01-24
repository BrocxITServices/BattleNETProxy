# Technisch Ontwerp Document

## Inleiding

Dit document beschrijft het technische ontwerp voor een Blazor C# API-koppeling met de Blizzard API die gebruik maakt van OAuth.

## Inhoudsopgave

1. [HTTP Request Client](#item-one)
2. [URL Builder](#item-two)
3. [OAuth Authenticatie](#item-three)
4. [Token Ophalen](#item-four)
5. [Endpoints en Parameters](#item-five)
6. [Foutafhandeling](#item-six)
8. [Conclusie](#item-seven)
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
### Error handeling

De applicatie zal error's afhandelen die kunnen optreden tijdens het proces, zoals netwerkfouten, API-fouten, enz. Deze fouten worden gelogd voor debugging en de gebruiker wordt op de hoogte gebracht van het probleem op een manier die hun begrijpen. 
<a id="item-seven"></a>
## Conclusie

Dit technisch ontwerp document biedt een gedetailleerd overzicht van de technische aspecten van de applicatie. Het volgende stadium van dit project zou de implementatie van deze ontwerpen zijn. Bij vragen of opmerkingen kunt u contact opnemen met de auteur van dit document.



