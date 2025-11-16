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



bool running = true;






if (File.Exists("User.txt"))
{

    string[] lines2 = File.ReadAllLines("User.txt");
    foreach (string line in lines2)
    {

        string[] data = line.Split(",");
        users.Add(new(data[0], data[1]));
    }


}

if (File.Exists("Room.txt"))
{
    string[] lines1 = File.ReadAllLines("Room.txt");
    foreach (string line in lines1)
    {
        if (string.IsNullOrWhiteSpace(line)) continue; // hoppar över tomma rader
        string[] data = line.Split(",");
        string guest = data[0];
        string number = data[1];
        string statuss = data[2];


        if (Enum.TryParse(statuss, out RoomStatus status))
        {
            rooms.Add(new Room(guest, number, status));
        }

    }

}
while (running)
{
    if (active_user == null)

    {
        Console.Clear();
        System.Console.WriteLine(" Username : ");
        string username = Console.ReadLine();

        Console.Clear();
        System.Console.WriteLine(" Password : ");
        string password = Console.ReadLine();

        foreach (User user in users)
        {
            if (user.TryLogin(username, password))
            {
                active_user = user;
                System.Console.WriteLine($" Welcome {user.username}!");
                break;

            }
        }


        if (active_user == null)
        {
            System.Console.WriteLine(" Wrong username or password");
            Console.ReadKey();
            continue;
        }
        Console.ReadKey();
        continue;
    }


    else
    {
        Console.Clear();
        System.Console.WriteLine(" -- Receptionist menu -- ");
        System.Console.WriteLine(" 1. Show a list of all rooms that guests are temporarily staying in");
        System.Console.WriteLine(" 2. Show all the empty rooms");
        System.Console.WriteLine(" 3. Book a guest into an available room");
        System.Console.WriteLine(" 4. Check out a guest from an occupied room");
        System.Console.WriteLine(" 5. Mark a room as temporarily unavailable");
        System.Console.WriteLine(" 6. Log out");
        string menu = Console.ReadLine()!;
        switch (menu)
        {
            case "1":
                Console.Clear();
                foreach (Room room in rooms)
                {
                    if (room.Status == RoomStatus.occupied)
                    {
                        System.Console.WriteLine(room.Status);
                        System.Console.WriteLine(room.GuestName + " is living here");
                        System.Console.WriteLine(room.RoomNumber + "room nummber");
                    }
                }
                Console.ReadLine();
                break;

            case "2":
                Console.Clear();
                System.Console.WriteLine(" Available rooms : ");
                foreach (Room room in rooms)
                    if (room.Status == RoomStatus.available)
                    {
                        Console.WriteLine(room.Status);
                        System.Console.WriteLine(room.RoomNumber + "room nummber");
                    }


                Console.ReadLine();
                break;

            case "3":
                Console.Clear();
                System.Console.WriteLine(" - Available rooms -");
                foreach (Room room in rooms)
                {
                    if (room.Status == RoomStatus.available)
                    {
                        System.Console.WriteLine($"Room {room.RoomNumber}");
                    }
                }


                System.Console.WriteLine(" Enter which room number you would like to book : ");
                string number = Console.ReadLine();
                Console.Clear();

                System.Console.WriteLine(" Enter guest name : ");
                string name = Console.ReadLine();

                Room selectedRoom = null;

                foreach (Room room in rooms)
                {
                    if (room.RoomNumber == number && room.Status == RoomStatus.available)
                    {
                        selectedRoom = room;
                        break; // slutar leta när vi hittat rätt rum

                    }


                }

                if (selectedRoom != null)
                {
                    selectedRoom.GuestName = name;
                    selectedRoom.Status = RoomStatus.occupied;

                    List<string> lines = new List<string>();

                    foreach (Room room in rooms)
                    {
                        string line = room.RoomNumber + "," + room.GuestName + "," + room.Status;
                        lines.Add(line);
                    }

                    File.WriteAllLines("Room.txt", lines);
                    System.Console.WriteLine($"Booked room {selectedRoom.RoomNumber} for {selectedRoom.GuestName} ");

                }

                else
                {
                    System.Console.WriteLine(" Room not found or not available");
                }

                System.Console.WriteLine(" Press any key to continue!");
                Console.ReadKey();
                break;

/*
                case "4":
                Console.Clear();
                System.Console.WriteLine("  Occupied rooms : ");

*/



        }


    }


}







