using AccessDB.DTO;
using ModelLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLogic.Models
{
    public class Flow
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

        public Flow(FlowDTO dto)
        {
            TimeReceived = dto.TimeReceived;
            TimeFlowStart = dto.TimeFlowStart;
            SequenceNum = dto.SequenceNum;
            SamplingRate = dto.SamplingRate;
            SamplerAddress = IpTransformer.MaskToString(Encoding.ASCII.GetBytes(dto.SamplerAddress));
            SrcAddr = IpTransformer.MaskToString(Encoding.ASCII.GetBytes(dto.SrcAddr));
            DstAddr = IpTransformer.MaskToString(Encoding.ASCII.GetBytes(dto.DstAddr));
            SrcAS = dto.SrcAS;
            DstAS = dto.DstAS;
            EType = dto.EType;
            Proto = dto.Proto;
            SrcPort = dto.SrcPort;
            DstPort = dto.DstPort;
            Bytes = dto.Bytes;
            Packets = dto.Packets;
        }

        public Flow() { }
    }
}
