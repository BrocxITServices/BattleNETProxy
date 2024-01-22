 **Inhoudsopgave:**

 1 [Inleiding](#item-one)

 2 [Doelstellingen](#item-two)

 3 [Functionele specificaties](#item-three)



 

 <!-- headings -->

 <a id="item-one"></a>
 ### Inleiding
Dit document beschrijft de functionele vereisten voor de ontwikkeling van een API-koppeling met de Battle.net API. Het doel van de API-koppeling is het verkrijgen van karaktergegevens via een MudBlazor-website.

 
 <a id="item-two"></a>
 ### Doelstellingen
Het doel van dit project is het realiseren van een mudblazor website waar je zonder in te loggen met je account gegevens jouw huidige statistieken kan ophalen en bekijken.

 

 <a id="item-three"></a>
 ### Functionele specificaties
 #### 3.1 Ophalen van karakter gegevens
 De gebruiker van ons platform kan doormiddel van een placeholder zijn karakter naam invullen en opsturen, zodra dit binnenkomt op onze API koppeling worden de statistieken van de gebruiker opgehaald, dit betreft het volgende: Name, Id, Level, Class, Achievements, Bank value. Dit wordt vervolgens getoond op de website en kan de gebruiker zijn statistieken inzien.

 #### 3.2 Authentication flow
 Door het implementeren van OAuth en het handhaven van best-practices zorgen we ervoor dat de client secret voor de authorization server geheim gehouden word en er geen persoonlijke data wordt weergeven aan de end user. 

 #### 3.3 Filteren op naam
 De gebruiker heeft de mogelijkheid om zijn karakter gegevens te sorteren op naam, hierdoor kan de gebruiker zijn statistieken inzien per karakter.

 #### 3.4 User interface
 Moet nog besproken worden.

#### 3.5 Error handling & user feedback
 Door op een gebruikersvriendelijke manier error handling toe te passen, ontvangt de eindgebruiker de juiste feedback bij problemen, zoals bijvoorbeeld een time-out. Als het ophalen van gegevens te lang duurt of als de API offline is, wordt de gebruiker voorzien van een melding zoals 'Probeer het later opnieuw'.

#### 3.6 Modulair en Generieke opzet
 Het vroegtijdig ontwikkelen van modulaire functionaliteit maakt het mogelijk om deze in de toekomst te hergebruiken, bijvoorbeeld bij een migratie van de website naar Azure.
