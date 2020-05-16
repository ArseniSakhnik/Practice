using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }

    }
}
