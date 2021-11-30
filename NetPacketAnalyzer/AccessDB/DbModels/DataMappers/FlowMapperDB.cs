using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;
using DataObjects.Utilities;
using AccessDB.DbModels.ClickHouse;

namespace AccessDB.DbModels.DataMappers
{
    static class FlowMapperDB
    {
        static public Flow MapClickHouse(FlowClickHouse dto)
        {
            Flow flow = new Flow();
            flow.TimeReceived = dto.TimeReceived;
            flow.TimeFlowStart = dto.TimeFlowStart;
            flow.SequenceNum = dto.SequenceNum;
            flow.SamplingRate = dto.SamplingRate;
            flow.SamplerAddress = IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.SamplerAddress));
            flow.SrcAddr = IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.SrcAddr));
            flow.DstAddr = IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.DstAddr));
            flow.SrcAS = dto.SrcAS;
            flow.DstAS = dto.DstAS;
            flow.EType = dto.EType;
            flow.Proto = dto.Proto;
            flow.SrcPort = dto.SrcPort;
            flow.DstPort = dto.DstPort;
            flow.Bytes = dto.Bytes;
            flow.Packets = dto.Packets;
            return flow;
        }

        static public FlowClickHouse MapToClickHouse(Flow flow)
        {
            FlowClickHouse dto = new FlowClickHouse();
            dto.TimeReceived = flow.TimeReceived;
            dto.TimeFlowStart = flow.TimeFlowStart;
            dto.SequenceNum = flow.SequenceNum;
            dto.SamplingRate = flow.SamplingRate;
            dto.SamplerAddress = Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.SamplerAddress));
            dto.SrcAddr = Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.SrcAddr));
            dto.DstAddr = Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.DstAddr));
            dto.SrcAS = flow.SrcAS;
            dto.DstAS = flow.DstAS;
            dto.EType = flow.EType;
            dto.Proto = flow.Proto;
            dto.SrcPort = flow.SrcPort;
            dto.DstPort = flow.DstPort;
            dto.Bytes = flow.Bytes;
            dto.Packets = flow.Packets;
            return dto;
        }
    }
}
