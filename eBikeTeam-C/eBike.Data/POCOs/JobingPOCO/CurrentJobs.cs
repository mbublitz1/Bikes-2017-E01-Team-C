using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs.JobingPOCO
{
    public class CurrentJobs
    {
        public int JobID { get; set; }
        public DateTime In {get; set;}
        public DateTime? Started { get; set; }
        public DateTime? Done { get; set; }
        public string Customer { get; set; }
        public string ContactNumber { get; set; }
    }

}
