using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication15.Pages
{
    public class Request
    {
        [Key]
        public string username { get; set; }
        public bool status { get; set; }
    }
}
