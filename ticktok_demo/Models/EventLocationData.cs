using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticktok_demo.Models
{
    public class EventLocationData
    {
        public string empId { get; set; }
        public DateTime eventTime { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public int eventId { get; set; }

    }
}