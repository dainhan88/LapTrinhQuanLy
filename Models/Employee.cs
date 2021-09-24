using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTrinhQuanLy.Models
{
    public class Employee : Person
    {
        public string Companny { get; set; }
        public string Address { get; set; }
    }
}