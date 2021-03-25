using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class User
    {
        [Required, StringLength(20)]
        public string Key { get; set; }
        [Required, StringLength(1000)]
        public string Value { get; set; }
    }
}
