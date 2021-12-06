using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.DTO;
using DataObjects.Models;

namespace WebApplication.DataMappers
{
    class FlowMapperDTO
    {
        static public Flow MapToInner(FlowDTO dto)
        {
            Flow flow = new Flow(TimeReceived: dto.TimeReceived,
                                    TimeFlowStart: dto.TimeFlowStart,
                                    SequenceNum: dto.SequenceNum,
                                    SamplingRate: dto.SamplingRate,
                                    SamplerAddress: dto.SamplerAddress,
                                    SrcAddr: dto.SrcAddr,
                                    DstAddr: dto.DstAddr,
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

        static public FlowDTO MapToDto(Flow flow)
        {
            FlowDTO dto = new FlowDTO(TimeReceived: flow.TimeReceived,
                                        TimeFlowStart: flow.TimeFlowStart,
                                        SequenceNum: flow.SequenceNum,
                                        SamplingRate: flow.SamplingRate,
                                        SamplerAddress: flow.SamplerAddress,
                                        SrcAddr: flow.SrcAddr,
                                        DstAddr: flow.DstAddr,
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
