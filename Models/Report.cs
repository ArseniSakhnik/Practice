using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReportName { get; set; }
        public Scientist Scientist { get; set; }
        public string Text { get; set; }
        public DateTime ReportDate { get; set; }
        public bool IsPublished { get; set; } = false;

    }
}
