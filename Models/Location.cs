using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public Country Country { get; set; }
        public List<Conference> Conferences { get; set; }

        public Location()
        {
            Conferences = new List<Conference>();
        }
    }
}
