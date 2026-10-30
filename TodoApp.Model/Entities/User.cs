using System.ComponentModel.DataAnnotations;
namespace TodoApp.Model.Entities;


public class User
{
    [Key]
    public int UserID { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }

}