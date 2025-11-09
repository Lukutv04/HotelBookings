namespace App;

public class User

{
    public string username;
    public string password;

    public User(string Username, string Password)

    {
        username = Username;
        password = Password;
    }

     public bool TryLogin(string inputUsername, string inputPassword)
    {
        return username == inputUsername && password == inputPassword;
    }
}
