using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.Models
{
    public class Flow
    {
        public DateTime TimeReceived { get; }
        public DateTime TimeFlowStart { get; }
        public int SequenceNum { get; }
        public int SamplingRate { get; }
        public string SamplerAddress { get; }
        public string SrcAddr { get; }
        public string DstAddr { get; }
        public int SrcAS { get; }
        public int DstAS { get; }
        public int EType { get; }
        public int Proto { get; }
        public int SrcPort { get; }
        public int DstPort { get; }
        public int Bytes { get; }
        public int Packets { get; }

        public Flow(DateTime TimeReceived, DateTime TimeFlowStart, int SequenceNum,
                              int SamplingRate, string SamplerAddress, string SrcAddr,
                              string DstAddr, int SrcAS, int DstAS, int EType, int Proto,
                              int SrcPort, int DstPort, int Bytes, int Packets)
        {
            this.TimeReceived = TimeReceived;
            this.TimeFlowStart = TimeFlowStart;
            this.SequenceNum = SequenceNum;
            this.SamplingRate = SamplingRate;
            this.SamplerAddress = SamplerAddress;
            this.SrcAddr = SrcAddr;
            this.DstAddr = DstAddr;
            this.SrcAS = SrcAS;
            this.DstAS = DstAS;
            this.EType = EType;
            this.Proto = Proto;
            this.SrcPort = SrcPort;
            this.DstPort = DstPort;
            this.Bytes = Bytes;
            this.Packets = Packets;
        }
    }
}
