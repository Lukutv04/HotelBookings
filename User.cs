namespace App;

public class User // user-klassen som representerar en användare/receptionist

{
    public string username; // användarnamn
    public string password; // lösenord


// används för att skapa en ny användare
    public User(string Username, string Password)

    {
        username = Username;
        password = Password;
    }


// metod som kollar av om inmatade uppgifter stämmer överens med användarens sparade uppgifter,
// returnerar true om stämmer annars false
     public bool TryLogin(string inputUsername, string inputPassword)
    
        => username == inputUsername && password == inputPassword; 
    }
