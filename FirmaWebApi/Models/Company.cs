using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FirmaWebApi.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Country { get; set; }
       
        public virtual List<Worker> Workers { get; set; }
    }
}