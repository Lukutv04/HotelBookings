namespace App; 

public class Room

{
    public string RoomNumber;
    public string GuestName;
    public RoomStatus Status;

    public Room(string guestName, string roomNumber, RoomStatus status)
    {
        RoomNumber = roomNumber;
        GuestName = guestName;
        Status = status;

    }
}

public enum RoomStatus
{
    available,
    occupied,
    currently_unavailable, 

}