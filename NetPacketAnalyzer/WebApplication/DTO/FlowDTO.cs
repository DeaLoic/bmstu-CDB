using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.DTO
{
    public class FlowDTO
    {
        public DateTime TimeReceived { get; set; }
        public DateTime TimeFlowStart { get; set; }
        public int SequenceNum { get; set; }
        public int SamplingRate { get; set; }
        public string SamplerAddress { get; set; }
        public string SrcAddr { get; set; }
        public string DstAddr { get; set; }
        public int SrcAS { get; set; }
        public int DstAS { get; set; }
        public int EType { get; set; }
        public int Proto { get; set; }
        public int SrcPort { get; set; }
        public int DstPort { get; set; }
        public int Bytes { get; set; }
        public int Packets { get; set; }

        public FlowDTO() { }
    }
}
