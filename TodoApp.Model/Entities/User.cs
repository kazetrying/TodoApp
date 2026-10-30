using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TodoApp.Model.Entities;


public class User : ICreateInfor , IModifiInfor
{
    private DateTime _createdDate;
    private string _createdBy;
    private DateTime _modifiedDate;
    private string _modifiedBy;
    [Key]
    public int UserID { get; set; }
    [Column]
    public string Username { get; set; }
    [Column]
    public string Password { get; set; }
    [Column]
    public string Fullname { get; set; }
    public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
    public string CreatedBy { get => _createdBy; set => _createdBy = value; }
    public DateTime ModifiedDate { get => _modifiedDate; set => _modifiedDate = value; }
    public string ModifiedBy { get => _modifiedBy; set => _modifiedBy = value; }
}