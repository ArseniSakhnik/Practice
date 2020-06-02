using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Conference
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ConferenceName { get; set; }
        public string ConferenceDescription { get; set; }
        public DateTime StartOfConference { get; set; }
        public List<ScientistConference> ScientistConference { get; set; }
        public List<ReportConference> ReportConference { get; set; }
        public Location Location { get; set; }

        public Conference()
        {
            ScientistConference = new List<ScientistConference>();
        }
    }
}
