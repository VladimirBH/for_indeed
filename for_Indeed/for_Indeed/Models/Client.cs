using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace for_Indeed.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите фамилию пользователя")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите адрес пользователя")]
        public string Address { get; set; }
    }
}
