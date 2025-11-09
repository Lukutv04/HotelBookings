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
    while (running)



  if (File.Exists("Users.txt")) 
    {

        string[] lines2 = File.ReadAllLines("Users.txt");
        foreach (string line in lines2)
        {

            string[] data = line.Split(",");
            users.Add(new(data[0], data[1]));
        }



        
        {
            System.Console.WriteLine(" -- Receptionist menu -- ");
            System.Console.WriteLine(" 1. Show a list of all rooms that guests are temporarily staying in");
            System.Console.WriteLine(" 2. Show all the empty rooms");
            System.Console.WriteLine(" 3. Book a guest into an available room");
            System.Console.WriteLine(" 4. Check out a guest from an occupied room");
            System.Console.WriteLine(" 5. Mark a room as temporarily unavailable");
        }
    




  
    {
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






