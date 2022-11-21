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
            Flow flow = new Flow(TimeReceived: dto.TimeReceived,
                                    TimeFlowStart: dto.TimeFlowStart,
                                    SequenceNum: dto.SequenceNum,
                                    SamplingRate: dto.SamplingRate,
                                    SamplerAddress: IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.SamplerAddress)),
                                    SrcAddr: IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.SrcAddr)),
                                    DstAddr: IpTransformer.BytesToString(Encoding.ASCII.GetBytes(dto.DstAddr)),
                                    SrcAS: dto.SrcAS,
                                    DstAS: dto.DstAS,
                                    EType: dto.EType,
                                    Proto: dto.Proto,
                                    SrcPort: dto.SrcPort,
                                    DstPort: dto.DstPort,
                                    Bytes: dto.Bytes,
                                    Packets: dto.Packets
                                    );
            return flow;
        }

        static public FlowClickHouse MapToClickHouse(Flow flow)
        {
            FlowClickHouse dto = new FlowClickHouse(TimeReceived: flow.TimeReceived,
                                                    TimeFlowStart: flow.TimeFlowStart,
                                                    SequenceNum: flow.SequenceNum,
                                                    SamplingRate: flow.SamplingRate,
                                                    SamplerAddress: Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.SamplerAddress)),
                                                    SrcAddr: Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.SrcAddr)),
                                                    DstAddr: Encoding.ASCII.GetString(IpTransformer.StringToBytes(flow.DstAddr)),
                                                    SrcAS: flow.SrcAS,
                                                    DstAS: flow.DstAS,
                                                    EType: flow.EType,
                                                    Proto: flow.Proto,
                                                    SrcPort: flow.SrcPort,
                                                    DstPort: flow.DstPort,
                                                    Bytes: flow.Bytes,
                                                    Packets: flow.Packets
                                                );
            return dto;
        }
    }
}
