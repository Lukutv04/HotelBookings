namespace App; 

public class Room // rum-klassen som representerar ett hotellrum

{
    // variabler som innefattar information om rummet
    public string RoomNumber; // rummets nummer
    public string GuestName; // namnet på gästen som bor i rummet
    public RoomStatus Status; // statusen på rummet


// används när man skapar ett nytt rum
    public Room(string guestName, string roomNumber, RoomStatus status)
    {
        RoomNumber = roomNumber;
        GuestName = guestName;
        Status = status;

    }
}


// lista/enums över alla "tillstånd" ett rum kan ha
public enum RoomStatus
{
    available,
    occupied,
    currently_unavailable, 

}