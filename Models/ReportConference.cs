using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ReportConference
    {
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
