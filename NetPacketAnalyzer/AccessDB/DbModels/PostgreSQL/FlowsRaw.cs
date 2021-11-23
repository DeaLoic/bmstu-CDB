using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class FlowsRaw
    {
        public DateTime? Date { get; set; }
        public DateTime? Timereceived { get; set; }
        public DateTime? Timeflowstart { get; set; }
        public int? Sequencenum { get; set; }
        public int? Samplingrate { get; set; }
        public string Sampleraddress { get; set; }
        public string Srcaddr { get; set; }
        public string Dstaddr { get; set; }
        public int? Srcas { get; set; }
        public int? Dstas { get; set; }
        public int? Etype { get; set; }
        public int? Proto { get; set; }
        public int? Srcport { get; set; }
        public int? Dstport { get; set; }
        public int? Bytes { get; set; }
        public int? Packets { get; set; }
    }
}
