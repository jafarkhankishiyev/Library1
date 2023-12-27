using System;

public class Author
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    

    public Author(string name, string surname, DateTime birthday, string email, string mobile)
    {
        Name = name;
        Surname = surname;
        Birthday = birthday;
        Email = email;
        Mobile = mobile;
    }



}