using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LapTrinhQuanLy.Models
{
    public class Account
    {
        [Key]
        [Required (ErrorMessage = "use name is requeid.")]
        public string UseName { get; set; }
        [Required(ErrorMessage = "pass is requeid.")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
      
    }
}