## Info
- Du kommer se att jag deleteat branches fast man inte får. Jag fick problem med skapandet av GitHub actions (builden kunde inte identifiera mina solution-files), och därför testade jag att göra MÅNGA olika versioner och PR's.
  Det såg så fult ut att jag deleteade dessa branches, innan jag kom på att jag bara kunde stängt PRen istället. Så ingen kod-branch har deleteats, enbart branches med Yaml-filer.
- Jag är osäker på om jag verkligen fick till GitHub Actions så att testerna körs. För mig ser det bara ut som att den kör en build?
- Jag har inte implementerat att ha både en titel och en beskrivning eftersom jag anser att en ToDo är självförklarande.

## Setup
- Jag ville gärna kunna testa med en Db så jag har försökt läsa på och byggt en sorts localDb setup som ska startas automatiskt när du kör programmet. Vid tester ska Db:n ta bort och lägga till mock-data efter varje test.
  Jag har lite för lite erfarenhet för att avgöra om det verkligen blev en bra lösning.  
- Du ska kunna köra Core-projektet direkt utan setup av databas, om jag har läst Microsoft Docs rätt. 
- Det har hänt att det varit problem att köra i webbläsaren Chrome. Mozilla har funkat bra alltid. Jag har inte testat i Safari eller Edge. 
  
