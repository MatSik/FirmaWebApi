using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FirmaWebApi.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }

        [Required]
        [StringLength(15,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }
    }
}