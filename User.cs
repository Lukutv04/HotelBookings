namespace App;

public class User

{
    public string name;
    public string password;

    public User(string Name, string Password)

    {
        name = Name;
        password = Password;
    }

     public bool TryLogin(string inputName, string inputPassword)
    {
        return name == inputName && password == inputPassword;
    }
}
