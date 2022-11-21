using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.Models
{
    public class FlowFilters
    {
        public DateTime? MinTimeFlowStart { get; }
        public DateTime? MaxTimeFlowStart { get; }
        public string? SrcAddr { get; }
        public string? DstAddr { get; }
        public int? MinBytes { get; }
        public int? MaxBytes { get; }

        public FlowFilters(DateTime? MinTimeFlowStart, DateTime? MaxTimeFlowStart, string? SrcAddr,
                              string? DstAddr, int? MinBytes, int? MaxBytes)
        {
            this.MinTimeFlowStart = MinTimeFlowStart;
            this.MaxTimeFlowStart = MaxTimeFlowStart;
            this.SrcAddr = SrcAddr;
            this.DstAddr = DstAddr;
            this.MinBytes = MinBytes;
            this.MaxBytes = MaxBytes;
        }
    }
}
