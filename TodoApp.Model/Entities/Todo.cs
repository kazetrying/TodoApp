using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Model.Entities
{
    public class Todo : ICreateInfor, IModifiInfor
    {
        private DateTime _createdDate;
        private string _createdBy;
        private DateTime _modifiedDate;
        private string _modifiedBy;
        [Key]
       public int TodoId { get; set; }
       [Column]
       public string TodoName { get; set; }
       [Column]
       public DateTime StartDate { get; set; }
       [Column]
       public DateTime EndDate { get; set; }
       [ForeignKey("Group")]
       public int GroupId { get; set; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public DateTime ModifiedDate { get => _modifiedDate; set => _modifiedDate = value; }
        public string ModifiedBy { get => _modifiedBy; set => _modifiedBy = value; }
    }
}