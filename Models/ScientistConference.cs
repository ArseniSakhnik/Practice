using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ScientistConference
    {
        public int ScientistId { get; set; }
        public Scientist Scientist { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
