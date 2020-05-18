using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Scientist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<ScientistOrganization> ScientistOrganization { get; set; } 
        public List<Report> Reports { get; set; }
        public List<ScientistConference> ScientistConference { get; set; } 
        public Country Country { get; set; }

        public Scientist()
        {
            ScientistOrganization = new List<ScientistOrganization>();
            Reports = new List<Report>();
            ScientistConference = new List<ScientistConference>();
        }
        
    }
}
