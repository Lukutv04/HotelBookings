# HotelBookings

## Mitt program är uppbyggt med hjälåp av ett filsystem. Jag har två textfiler "User.txt" där alla användare/receptionister username och password sparas och "Room.txt" där uppgifter om rummen sparas och där står det vilket rum det är, vem som bor i rummet och statusen på rummet. Statusen kommer ifrån enums som jag har lagt till i Room.cs (available, occupied, currently unavailable). 

## För att kunna använda programmet så måste du logga in med dina uppgifter som ska finnas sparade i User.txt filen allstå ett username och ett lösenord. När du har loggat in så får du 6 olika alternativ som du kan göra som receptionist. 

### - Se en lista av alla rum som har gäster just nu
### - Se en lista av alla lediga rum
### - Boka in en gäst i ett ledigt rum
### - Checka ut en gäst från ett upptaget rum
### - Markera att ett rum , temporärt, inte är tillgängligt
### Logga ut

### När du som receptionist väljer att göra något av dessa valen så kollar programmet av i Room.txt filen och läser av rum för rum vilka som har vilken status osv. Allt är kopplat ihop med den textfilen. 


## val jag gjort och varför?

### använde mig endast av 2 klasser vilket var User och Room för det var enklast så och det fanns ingen mening till att ha fler. En som har koll på inloggning och en som har informationen om rummen. 

### anvvände mig av metoden TryLogin i User.cs för att samla all inloggningslogik på ett ställe så att jag inte behövde jämföra username och password i Program.cs tex. Enkel metod helt enkelt.

### använde mig av enum för att representera statusen på rummen rätt enkelt med fasta värden. Behövde inte oroa mig för att stava fel på status om man skriver det som en vanlig sträng tex. 


### använde mig av en del foreach - loopar för att enkelt gå igenom alla users eller rum så att den kollar varje rum för rum efter en viss status tex. 

### använde mig av listor för users och rooms för att enkelt spara flera objekt i en samling och för att enkelt kunna lägga till eller ta bort "objekt" ifrån listorna. 

### använde mig av switch(menu) och cases för att användaren ska ha en enkel meny med olika val den kan göra. Detta gör koden mer strukturerad ockspå för någon som läser den för varje case blir lätt att läsa till skillnad ifrån en massa if/else satser istället. 

### använde mig en del av break och continue för att enkelt kunna bryta en loop eller gå vidare/hoppa över vid rätt tillfällen tex när användaren gör fel inloggning. 

### använde mig av File.WriteAllLines() för att enkelt kunna uppdtarea textfilerna med ny info tex efter att man ändrat något i ett rum så skrivs hela listan med rum tillbaka till Room.txt.