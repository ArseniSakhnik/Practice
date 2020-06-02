using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsReportsAvailable { get; set; } = false;
        public bool IsOrganizationAvailable { get; set; } = false;
        public bool IsConfereceAvailable { get; set; } = false;
        public bool IsScientistAvailable { get; set; } = false;
        public bool IsLocalityAvailable { get; set; } = false;
        public bool IsUserAvialble { get; set; } = false;
        public bool IsWordReportAvailable { get; set; } = false;
        public bool IsCountryAvailable { get; set; } = false;
    }
}
