using App;

/*

- Logga in med uppgifter som ligger sparade i en fil
- Se en lista av alla rum som har gäster just nu
- Se en lista av alla lediga rum
- Boka in en gäst i ett ledigt rum
- Checka ut en gäst från ett upptaget rum
- Markera att ett rum , temporärt, inte är tillgängligt
*/





List<Room> rooms = new List<Room>(); // Ny lista med alla rum
List<User> users = new List<User>(); // Ny lista med alla användare
User? active_user = null; // Kollar om ingen är inloggad



bool running = true; // kör programmet så länge huvudloopen är true





//hela denna gör så att programmet kan läsa av alla användare ifrån User.txt filen
if (File.Exists("User.txt")) // kollar om filen "User.txt" existerar
{

    string[] lines2 = File.ReadAllLines("User.txt"); // Läser in alla rader i filen
    foreach (string line in lines2) // går igenom varje rad i filen
    {

        string[] data = line.Split(","); // delar upp username och password
        users.Add(new(data[0], data[1])); // skapar en ny user och lägger till i listan med hjälp av ett username och ett password
    }


}

// hela denna läser in alla rum ifrån Room.txt filen
if (File.Exists("Room.txt")) // kollar ifall Room.txt filen existerar
{
    string[] lines1 = File.ReadAllLines("Room.txt"); // läser in varje rad i filen
    foreach (string line in lines1) // går igenom varje rad
    {
        if (string.IsNullOrWhiteSpace(line)) continue; // hoppar över tomma rader
        string[] data = line.Split(","); // delar upp en rad i filen med 3 olika typer som jag skrev nedanför
        string guest = data[0]; // gäst namnet först
        string number = data[1]; // sedan rums-numret
        string statuss = data[2]; // och sista statusen på rummet


        if (Enum.TryParse(statuss, out RoomStatus status)) // omvandlar text till enum-värde
        {
            rooms.Add(new Room(guest, number, status)); // skapar nytt rum och lägger till i listan genom gästnamn, rumsnummer, status
        }

    }

}
while (running) // huvudloop som kör tills använadren avslutar hela programmet
{
    if (active_user == null) // om ingen är inloggad 

    {
        Console.Clear(); // tar bort all tidigare text osv
        System.Console.WriteLine(" Username : ");
        string username = Console.ReadLine(); // läser in användarnamnet

        Console.Clear();
        System.Console.WriteLine(" Password : ");
        string password = Console.ReadLine(); // läser in lösenordet

        foreach (User user in users) // går igenom alla användare
        {
            if (user.TryLogin(username, password)) // kontrollerar användarnamnet och lösenordet
            {
                active_user = user; // sätter använadren till en aktiv användare
                System.Console.WriteLine($" Welcome {user.username}!");
                break; // stoppar loopen när en match hittas

            }
        }


        if (active_user == null) // om ingen användare hittades
        {
            System.Console.WriteLine(" Wrong username or password");
            Console.ReadKey(); // användaren kan nu se felmeddelandet och trycka på vilken tangent som helst för att gå vidare
            continue; // hoppar tillbaka till att göra en ny inloggning
        }
      
    }


    else // om användaren nu är inloggad
    {
        Console.Clear();
        // meny för receptionisten :
        System.Console.WriteLine(" -- Receptionist menu -- ");
        System.Console.WriteLine(" 1. Show a list of all rooms that guests are temporarily staying in");
        System.Console.WriteLine(" 2. Show all the empty rooms");
        System.Console.WriteLine(" 3. Book a guest into an available room");
        System.Console.WriteLine(" 4. Check out a guest from an occupied room");
        System.Console.WriteLine(" 5. Mark a room as temporarily unavailable");
        System.Console.WriteLine(" 6. Log out");
        string menu = Console.ReadLine()!; // läser av vilket valk receptionisten gör
        switch (menu)
        {
            case "1":
                Console.Clear();
                System.Console.WriteLine("  Occupied rooms : ");

                int count_occupied = 0; // räknar antalet upptagna rum

                foreach (Room room in rooms) // går igenom rum för rum
                {
                    if (room.Status == RoomStatus.occupied) // om rummet är upptaget
                    {

                        count_occupied++;  // ökar talet varje gång den hittar ett upptaget rum
                        // får upp rumsnummer samt vilken gäst som bor där för de rum som är upptagna
                        System.Console.WriteLine($" Room Number : {room.RoomNumber}");
                        System.Console.WriteLine($" Guest Name : {room.GuestName}");
                        System.Console.WriteLine("----------------");
                    }
                }

                if(count_occupied == 0) // om inga upptagna rumm finns/hittades
                {
                    System.Console.WriteLine(" No rooms are currently occupied!" );
                }
                System.Console.WriteLine(" Press any key to continue! ");
                Console.ReadLine();
                break;

            case "2":
                Console.Clear();
                System.Console.WriteLine(" Available rooms : ");

                int count = 0; // räknar antalet lediga rum


                foreach (Room room in rooms)
                    if (room.Status == RoomStatus.available) // om rummet är ledigt
                    {
                        System.Console.WriteLine(room.Status);
                        System.Console.WriteLine($"Room {room.RoomNumber} ");
                        count++; // ökar varje gång vi hittar ett ledigt rum
                    }
                    
                    if(count == 0) // om inga lediga rum finns/hittades
                    {
                        System.Console.WriteLine(" No rooms available! ");
                    }

                System.Console.WriteLine("\n Press any key to continue!");
                Console.ReadLine();
                break;

            case "3":
                Console.Clear();
                System.Console.WriteLine(" - Available rooms -");
                foreach (Room room in rooms)
                {
                    if (room.Status == RoomStatus.available) // om rummet är ledigt
                    {
                        System.Console.WriteLine($"Room {room.RoomNumber}"); // skriver ut vilket rumn som är ledigt
                    }
                }


                System.Console.WriteLine(" Enter which room number you would like to book : ");
                string number = Console.ReadLine(); //läser in vilket rumsnummer som ska bokas
                Console.Clear();

                System.Console.WriteLine(" Enter guest name : ");
                string name = Console.ReadLine(); // läser in gästnamnet som ska bokas in i rummet

                Room selectedRoom = null; // håller rummet som ska bokas

                foreach (Room room in rooms) // hittar rätt rum genom att gå igenom dem
                {
                    if (room.RoomNumber == number && room.Status == RoomStatus.available) // om rummet är ledigt så bokas de
                    {
                        selectedRoom = room;
                        break; // slutar leta när vi hittat rätt rum

                    }


                }

                if (selectedRoom != null) // om rummet finns och är ledigt
                {
                    selectedRoom.GuestName = name; // ändrar till nytt gästnamn på rummet
                    selectedRoom.Status = RoomStatus.occupied; // ändrar statusen til upptaget istället för ledigt

                    List<string> rooooms = new List<string>(); // ny lista som sparas till filen

                    foreach (Room room in rooms)
                    {
                        string rooom = room.RoomNumber + "," + room.GuestName + "," + room.Status;
                        rooooms.Add(rooom); // lägger till varje rum i listan
                    }

                    File.WriteAllLines("Room.txt", rooooms); // skriver in uppdaterad lista till Room.txt filen
                    System.Console.WriteLine($"Booked room {selectedRoom.RoomNumber} for {selectedRoom.GuestName} ");

                }

                else // om inget rum hittas eller är ledigt
                {
                    System.Console.WriteLine(" Room not found or not available");
                }

                System.Console.WriteLine(" Press any key to continue!");
                Console.ReadKey();
                break;


            case "4":
                Console.Clear();
                System.Console.WriteLine("  Occupied rooms : ");

                foreach (Room room in rooms) // går igenom rum för rum för att se vilket som är upptaget
                {
                    if (room.Status == RoomStatus.occupied) // om rum är upptaget
                    {
                        System.Console.WriteLine($"Room {room.RoomNumber} - Guest : {room.GuestName} "); // skriver ut vem som bor i vilket rum

                    }
                }

                System.Console.WriteLine(" Enter the room number to check out a guest from :");
                string checkoutnumber = Console.ReadLine(); // användaren väljer ett rum genom att skriva rumsnumret och detta läser programmet in. 

                Room? checkoutroom = null; // håller rum som ska checkas ut
                foreach (Room room in rooms)
                {
                    if (room.RoomNumber == checkoutnumber && room.Status == RoomStatus.occupied) // om statusen är occupied
                    {
                        checkoutroom = room;
                        break;
                    }
                }


                if (checkoutroom != null) // kollar så de är rätt rum med ett värde
                {
                    System.Console.WriteLine($" Checking out guest {checkoutroom.GuestName} from room {checkoutroom.RoomNumber}");
                    checkoutroom.GuestName = ""; // gör så att gästnamnet frösvinner ifrån listan av rum eftersom den nu är utcheckad
                    checkoutroom.Status = RoomStatus.available; // ändrar statusen på rummet till available
                    List<string> roooms = new List<string>(); // uppdaterar listan med alla rum
                    foreach (Room room in rooms)
                    {
                        string rooom = room.RoomNumber + "," + room.GuestName + "," + room.Status;
                        roooms.Add(rooom); 
                    }

                    File.WriteAllLines("Room.txt", roooms); // lägger till listan/infon i textfilen
                    System.Console.WriteLine(" Guest checked out! ");
                }

                else // om inga upptagna rum finns/hittades
                {
                    System.Console.WriteLine(" No occupied rooms found! ");
                }

                System.Console.WriteLine(" Press any key to continue! ");
                Console.ReadKey();
                break;


                case "5":
                Console.Clear();
                System.Console.WriteLine(" Set room as temporary unavailable" );
                System.Console.WriteLine(" Current rooms : ");


                // visar alla rum och statusen på de
                foreach(Room room in rooms) 
                
                System.Console.WriteLine($" Room {room.RoomNumber} | {room.Status} | Guest : {(string.IsNullOrEmpty(room.GuestName) ? "-" : room.GuestName)} "); // metod som kontrollerar om en sträng inte har något värde eller är tom. Om den är tom eller 0 värde så returneras true, annars false om ett gästnamn dyker upp.

                    System.Console.WriteLine(" Enter which room number you would like to set as currently unavailable : ");
                    string setnumber = Console.ReadLine(); // användaren väljer vilket rum som ska sättas som currently unavailable och numret läses in. 

                    Room chosenroom = null; // valt rum
                    foreach(Room room in rooms)
                {
                    if(room.RoomNumber == setnumber)
                    {
                        chosenroom = room;
                        break;
                    }
                }


                if(chosenroom == null) // om rummet inte finns/hittades
                {
                    System.Console.WriteLine(" Room not found!");
                    Console.ReadKey();
                    break;
                }


                if(chosenroom.Status == RoomStatus.occupied) //om rummet är upptaget så går det ej att sätta som currently unavailable
                {
                    System.Console.WriteLine(" Cannot set an occupied room as unavailable! ");
                    System.Console.WriteLine(" \n Try again! ");
                    Console.ReadKey();
                    break;
                }

                // växlar mellan ledig och upptaget
                if (chosenroom.Status == RoomStatus.available) 
                chosenroom.Status = RoomStatus.currently_unavailable;

                else if (chosenroom.Status == RoomStatus.currently_unavailable)
                chosenroom.Status = RoomStatus.available;

                List<string> current = new List<string>(); // sparar infon till filen
                foreach (Room room in rooms)
                
                    current.Add(room.RoomNumber + "," + room.GuestName + "," + room.Status);
                    File.WriteAllLines("Room.txt", current); // uppdtarerar det valda rummets status i textfilen


                    System.Console.WriteLine($" Room {chosenroom.RoomNumber} is now {chosenroom.Status}!"); // berättar för användaren vilket rum som är currrently unavailable
                    System.Console.WriteLine(" Press any key to continue! ");
                    Console.ReadKey();
                    break;


                    case"6":
                    Console.Clear();
                    active_user = null; // tar bort den inloggade användaren
                    System.Console.WriteLine(" you have been loggedout, press any key to continue to log in!");
                    Console.ReadKey();
                    break;
                
                
        }


    }


}







