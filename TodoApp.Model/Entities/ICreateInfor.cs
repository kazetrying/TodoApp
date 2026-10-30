using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Model.Entities
{
    public interface ICreateInfor
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
}