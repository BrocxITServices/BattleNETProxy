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
7. [Conclusie](#item-seven)
<a id="item-one"></a>
### HTTP Request Client

De applicatie zal gebruik maken van een HTTP-client, zoals HttpClient in .NET, die GET-verzoeken verstuurt naar de Blizzard API. De client zal headers instellen voor authenticatie en andere benodigde informatie. De antwoorden van de API worden vervolgens verwerkt en de relevante gegevens worden geëxtraheerd.
<a id="item-two"></a>
### URL Builder
De URL Builder zal een basis-URL hebben voor de Blizzard API. Afhankelijk van de specifieke API-aanroep die nodig is, zal de builder de juiste endpoints en parameters toevoegen aan de basis-URL. Dit zorgt voor flexibiliteit en herbruikbaarheid van code.
<a id="item-three"></a>
##  Authorization Code Flow
De applicatie maakt gebruik van OAuth 2.0 voor authenticatie. Dit betekent dat de applicatie een toegangstoken van de Blizzard API zal aanvragen met behulp van de OAuth2-verzoek. Het ontvangen token wordt vervolgens gebruikt om geautoriseerde aanvragen te doen aan de API.

Om een `accessToken` op te halen, moet u een GET verzoek sturen naar:
```http
GET https://oauth.battle.net/authorize
```
met de authentication OAuth 2 moeten de volgende values in:
| Parameter | Value |
| --- | --- |
| GRANT TYPE | Authorization Code |
| AUTHORIZATION URL | https://oauth.battle.net/authorize |
| ACCESS TOKEN URL | https://oauth.battle.net/token |
| CLIENT ID | 'Application id' |
| CLIENT SECRET | 'Application secret'|
| CODE CHALLENGE METHOD | SHA-256 |
| REDIRECT URL | https://localhost:7141/|
| SCOPE | ![afbeelding](https://github.com/BrocxITServices/BattleNETProxy/assets/138728190/a479083d-1f34-4736-8f49-17408ed180bc)|
| STATE | Een unique value ik raad je aan om een guid te gebruiken. Hier kan je er een generaten https://guidgenerator.com/ |
| CREDENTIALS | As Basic Auth Header (default) |

Wanneer dit verzoek wordt verzonden, wordt de gebruiker omgeleid naar de Blizzard-website om in te loggen. Daarna geeft de gebruiker de applicatie toestemming om zijn informatie te gebruiken op basis van de scope die we gebruiken. Als alles goed gaat, zou u een token moeten ontvangen die 24 uur geldig is en nu wordt gebruikt voor alle GET-verzoeken waarvoor gebruikersinformatie nodig is.

Voor informatie over OAuth graag kijken naar https://develop.battle.net/documentation/guides/using-oauth 
De Authorize URL: https://oauth.battle.net/authorize
De Access token URL: https://oauth.battle.net/token
Voor nu heb je de authorized credentials nodig voor de volgende Get request:

    GET /oauth/userinfo
    GET /profile/user/wow
    GET /profile/user/wow/protected-character/{realm-id}-{character-id}
    GET /profile/user/wow/collections
    GET /profile/user/wow/collections/pets
    GET /profile/user/wow/collections/mounts

<a id="item-four"></a>
### Client Credentials Flow
De applicatie maakt gebruik van OAuth 2.0 voor authenticatie. Dit betekent dat de applicatie een toegangstoken van de Blizzard API zal aanvragen met behulp van de OAuth2-verzoek. Het ontvangen token wordt vervolgens gebruikt om geautoriseerde aanvragen te doen aan de API.

Hier zijn de stappen die betrokken zijn bij de Client Credentials Flow:
1. Om een `serverToken` op te halen, moet u een `POST` verzoek sturen naar:
```http
POST https://oauth.battle.net/token
```
Dit verzoek ziet er zo uit:
| Parameter | Value |
| --- | --- |
| GRANT TYPE | client_credentials |
| AUTHORIZATION: Basic| `clientId`:`clientSecret`
| CLIENT ID | 'Application id' |
| CLIENT SECRET | 'Application secret'|

2. **Token respons**: Als het verzoek succesvol is, zal de Blizzard API een JSON-object retourneren dat het toegangstoken bevat. Het toegangstoken kan vervolgens worden gebruikt om geautoriseerde aanvragen te doen aan de Blizzard API voor 'Game Data API' request deze wordt opgeslagen als een `server_token`

Het is belangrijk op te merken dat toegangstokens die zijn verkregen via de Client Credentials Flow geen toegang hebben tot endpoints die gebruikersspecifieke gegevens vereisen. Deze tokens zijn bedoeld voor server-tot-server interacties.

Vergeet niet dat toegangstokens een beperkte levensduur hebben en na een bepaalde periode verlopen. Volgens de documentatie hebben toegangstokens een levensduur van 24 uur. Na deze periode moet een nieuw toegangstoken worden aangevraagd.
<a id = "item-test"></a>
# Namespacing
Wij maken gebruik van drie soorten namespacing:
Static: Gebruik je bij 
Dynamic
Profile
<a id="item-five"></a>
# Endpoints en Parameters

De applicatie zal verschillende endpoints van de Blizzard API gebruiken, afhankelijk van de benodigde gegevens. De benodigde parameters voor elk endpoint worden dynamisch toegevoegd door de URL Builder. De gegevens van de API-respons worden genormaliseerd en opgeslagen in een gestructureerd formaat voor verdere verwerking.
De base url = https://region.api.blizzard.com
je vervangt het woord `region` voor welke server je wilt gebruiken de ondersteundende regions zijn:
`eu` voor Europa
`us` voor Noord America
`kr` voor Korea
`tw` voor Taiwan
## Game Data Requests

Deze verzoeken hebben betrekking op game-gerelateerde gegevens. Deze request stuur je op met een `server_token ` met de auth type `Bearer token`
### Get Creature
Returns a creature by ID.
```http
GET /data/wow/creature/{creatureId} 
```

| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
|`{creatureId}`| `int`|:heavy_check_mark:| The ID of the creature|
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

### Get Mount
Returns a mount by ID.

```http
GET /data/wow/mount/{mountId} 
```

| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `realmSlug`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `characterName`  | `string` |   :heavy_check_mark:       | The locale to reflect in localized data.  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

### Get Item
Returns an item by ID.
```http
GET /data/wow/item/{itemId} 
```
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `{itemId}`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |
## Profile Data Request
Deze verzoeken hebben betrekking op gebruikersspecifieke gegevens en vereisen een `access_token` en de juiste `scope`
Deze request stuur je op met een `access_token` met de auth type `Bearer token`

### Get Account
Returns a profile summary for an account.
```http
GET /profile/user/wow 
```
`scope` wow.profile
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |
### Get Mount Collection
Summary of mounts collected
```http
GET /profile/user/wow/collections/mounts  
```
`scope` wow.profile
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

response 
```json
{
	"_links": {
		"self": {
			"href": "https://eu.api.blizzard.com/profile/user/wow/collections/mounts?namespace=profile-eu"
		},
		"user": {
			"href": "https://eu.api.blizzard.com/profile/user"
		},
		"profile": {
			"href": "https://eu.api.blizzard.com/profile/user/wow?namespace=profile-eu"
		}
	},
	"mounts": [
		{
			"mount": {
				"key": {
					"href": "https://eu.api.blizzard.com/data/wow/mount/6?namespace=static-10.2.5_52554-eu"
				},
				"name": "Brown Horse",
				"id": 6
			}
		},
```
uit deze response wil je de name en de id opslaan

### Get Account
Returns a profile summary for an account.
```http
GET /profile/user/wow 
```
`scope` wow.profile
| Parameter | Type     | Required | Description                |
| :-------- | :------- | :------  | :------------------------- |
| `region`  | `string` |    :heavy_check_mark:   | Your region  |
| `namespace`| `string`|    :heavy_check_mark:   | The namespace to use to locate this document  |
| `locale`  | `string` |          | The locale to reflect in localized data.  |

## Locale parameter

Waneer je een request stuur zonder specific een locale value krijg je default value waardoor je de response terug krijgt in alle talen om het in een taal te krijgen moet je dus een value geven aan de locale parameter dit zijn je opties:
### North America
- API
  - en_US (English)
  - es_MX (Mexican Spanish)
  - pt_BR (Portuguese)

### Europe
- API
  - en_GB (English)
  - es_ES (Spanish)
  - fr_FR (French)
  - ru_RU (Russian)
  - de_DE (German)
  - pt_PT (Portuguese)
  - it_IT (Italian)

Wanneer je een request doet kijk dan goed naar de url als jij bijvoorbeeld gebruik maakt van `https://eu.api.blizzard.com/profile/user/wow` dan kan je geen gebruikt maken can de locale van North Amerika alleen van je eigen region
<a id="item-six"></a>
## Data Normalization

### Doel

Het doel van data normalisatie in deze API-koppeling is om ervoor te zorgen dat de gegevens die worden uitgewisseld tussen onze applicatie en de Blizzard API consistent en betrouwbaar zijn.

### Proces

Het proces van data normalisatie in deze API-koppeling zal bestaan uit de volgende stappen:

1. **Identificatie van Data Anomalieën**: We zullen een reeks geautomatiseerde tests implementeren die regelmatig worden uitgevoerd om inconsistenties of fouten in de gegevens te identificeren. Deze tests zullen worden geschreven in C# en zullen gebruik maken van de ingebouwde testmogelijkheden van het .NET framework.

2. **Adressering van Data Anomalieën**: Zodra anomalieën zijn geïdentificeerd, zullen we strategieën implementeren om deze aan te pakken en te corrigeren. Dit kan het opschonen van de gegevens omvatten, het aanpassen van de manier waarop we gegevens van de Blizzard API ophalen, of het aanpassen van de manier waarop we gegevens in onze eigen systemen opslaan.

3. **Implementatie van Normalisatie**: We zullen normalisatie technieken toepassen om redundantie te verminderen en gegevens te standaardiseren. Dit zal worden bereikt door het implementeren van een reeks normalisatie regels in onze API-koppeling code. Deze regels zullen worden toegepast wanneer gegevens worden ontvangen van de Blizzard API, voordat ze worden opgeslagen in onze eigen systemen.

### Verwachte Resultaten

Door data normalisatie toe te passen, verwachten we een verbetering van de gegevenskwaliteit, een vermindering van de redundantie en een verhoogde efficiëntie van onze API-koppeling met de Blizzard API.

<a id="item-seven"></a>
## Conclusie

Dit technisch ontwerp document biedt een gedetailleerd overzicht van de technische aspecten van de applicatie. Het volgende stadium van dit project zou de implementatie van deze ontwerpen zijn. Bij vragen of opmerkingen kunt u contact opnemen met de auteur van dit document.



