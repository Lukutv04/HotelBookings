namespace App;

public class Room

{
    public string roomnumber;
    public string user;
    public string status;

    public Room(string Roomnumber, string User, string Status)
    {
        roomnumber = Roomnumber;
        user = User;
        status = Status;

    }
}

public enum status
{
    available,
    occupied,
    cleanup,
}