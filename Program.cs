using App;

/*

- Logga in med uppgifter som ligger sparade i en fil
- Se en lista av alla rum som har gäster just nu
- Se en lista av alla lediga rum
- Boka in en gäst i ett ledigt rum
- Checka ut en gäst från ett upptaget rum
- Markera att ett rum , temporärt, inte är tillgängligt
*/





List<Room> rooms = new List<Room>();
List<User> users = new List<User>();
User? active_user = null;


    
    bool running = true;


if (File.Exists("Room.txt"))
{
    string[] lines = File.ReadAllLines("Room.txt");
     foreach(string line in lines)
    {
        if (string.IsNullOrWhiteSpace(line)) continue; // hoppar över tomma rader
        string[] data = line.Split(",");
        string guest = data[0];
        string number = data[1];
        string statuss = data[2];
    }
    
}



if (File.Exists("User.txt"))
{

    string[] lines = File.ReadAllLines("User.txt");
    foreach (string line in lines)
    {

        string[] data = line.Split(",");
        users.Add(new(data[0], data[1]));
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
                return;
            }
        }
        System.Console.WriteLine(" Wrong username or password");
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
        string menu = Console.ReadLine();
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
                foreach (Room room in rooms)
                    if (room.Status == RoomStatus.available)
                    {
                        Console.WriteLine(room.Status);
                        System.Console.WriteLine(room.RoomNumber + "room nummber");
                    }

                System.Console.WriteLine(" What room are you do you want to book for a guest? ");
                string number = Console.ReadLine()!;
                Console.Clear();
                System.Console.WriteLine(" What name would you like to book the room for? ");
                string name = Console.ReadLine();
        }


    }


}
  
  





