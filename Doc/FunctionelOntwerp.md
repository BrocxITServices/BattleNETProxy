 **Inhoudsopgave:**

 1 [Inleiding](#item-one)

 2 [Doelstellingen](#item-two)

 3 [Functionele specificaties](#item-three)

 4 [MoSCoW Analyse](#item-four)



 

 <!-- headings -->

 <a id="item-one"></a>
 ### Inleiding
Dit document beschrijft de functionele vereisten voor de ontwikkeling van een API-koppeling met de Battle.net API. Het doel van de API-koppeling is het verkrijgen van karaktergegevens via een MudBlazor-website.

 
 <a id="item-two"></a>
 ### Doelstellingen
Het doel van dit project is het ontwikkelen van een modulair en generieke api-koppeling met de battle.net API. Dit maakt het mogelijk om in de toekomst de api-koppeling te hergebruiken, bijvoorbeeld bij een migratie naar Azure. En zou je ook de api-koppeling kunne gebruiken voor andere doeleinden.

 
 <a id="item-three"></a>
 ### Functionele specificaties
 #### 3.1 Ophalen van karakter gegevens
 De gebruiker van ons platform kan doormiddel van een placeholder zijn karakter naam invullen en opsturen, zodra dit binnenkomt op onze API koppeling worden de statistieken van de gebruiker opgehaald, dit betreft het volgende: `Name`, `Id`, `Level`, `Class`, `Achievements`, `Bank value`. Dit wordt vervolgens getoond op de website en kan de gebruiker zijn statistieken inzien.

 #### 3.2 Authentication flow
 Door het implementeren van OAuth en het handhaven van best-practices zorgen we ervoor dat de client secret voor de authorization server geheim gehouden word en er geen persoonlijke data wordt weergeven aan de end user. 

 #### 3.3 Filteren op naam
 De gebruiker heeft de mogelijk om zijn karakters te sorteren op naam, hierdoor kan je de statistieken per karakter inzien zoals de opgehaalde karakter gegevens bij 3.1.

 #### 3.4 User interface
 Moet nog besproken worden.

#### 3.5 Error handling & user feedback
 Als de API niet meer reageert of zich anders gedraagt moeten we dit correct afhandelen, dit doen we door de gebruiker de juiste feedback te geven door bijvoorbeeld een bericht te tonen zoals 'Probeer het later opnieuw'

#### 3.6 SOLID Principles
 Door de SOLID principles te handhaven tijdens de ontwikkeling van de API-koppeling zorgen we ervoor dat de code geen afhankelijkheden heeft makkelijk te lezen is en het aanpassen en testen van de code eenvoudiger wordt. Ook is dit belangrijk voor door ontwikkeling aan de api-koppeling.
 
De key componenten van de SOLID Principles:
- **S**ingle Responsibility Principle.
- **O**pen-closed principle.
- **L**iskov substitution principle.
- **I**nterface segregation principle.
- **D**ependency inversion principle. 


 <a id="item-four"></a>
#### 4 MoSCoW Analyse
| **Nummer** |   **Omschrijving specificatie**  |                            **Opmerkingen**                            | **MoSCoW** |
|:----------:|:--------------------------------:|:---------------------------------------------------------------------:|:----------:|
|      1     | Ophalen van karakter gegevens    | Ophalen van bepaalde informatie van karakter zie documentatie         |      M     |
|      2     | Toepassen van authenticatie flow |                                                                       |      M     |
|      3     | Toepassen van error handling     | Gebruiksvriendelijk afhandelen van errors bijv: time-out, max-request |      M     |
|      4     | Modulaire en generieke opzet     | Implementeren van SOLID principles                                    |      S     |
|      5     | Filteren van karakter gegevens   | Het tonen van data op als voorbeeld 'Naam' filter.                    |      S     |
|      6     | User-interface ontwerp           | Moet nog besproken worden                                             |      S     |
