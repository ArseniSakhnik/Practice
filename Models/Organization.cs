using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrganizationName { get; set; } 
        public List<ScientistOrganization> ScientistOrganization { get; set; }
        public Organization()
        {
            ScientistOrganization = new List<ScientistOrganization>();
        }
    }
}
