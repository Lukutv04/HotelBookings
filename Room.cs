namespace App;

public class Room

{
    public string RoomNumber;
    public string GuestName;
    public string Status;

    public Room(string roomNumber, string guestName, string status)
    {
        RoomNumber = roomNumber;
        GuestName = guestName;
        Status = status;

    }
}

public enum Status
{
    available,
    occupied,
    currently_unavailable, 

}