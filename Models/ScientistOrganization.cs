using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ScientistOrganization
    {
        public int ScientistId { get; set; }
        public Scientist Scientist { get; set; }


        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
