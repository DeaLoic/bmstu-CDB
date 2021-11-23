﻿using AccessDB.DbModels.PostgreSQL;
using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
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

        public FlowDTO(IDataReader reader)
        {
            TimeReceived = reader.GetDateTime(reader.GetOrdinal("TimeReceived"));
            TimeFlowStart = reader.GetDateTime(reader.GetOrdinal("TimeFlowStart"));

            SequenceNum = reader.GetInt32(reader.GetOrdinal("SequenceNum"));
            SamplingRate = reader.GetInt32(reader.GetOrdinal("SamplingRate"));

            SamplerAddress = reader.GetString(reader.GetOrdinal("SamplerAddress"));
            SrcAddr = reader.GetString(reader.GetOrdinal("SrcAddr"));
            DstAddr = reader.GetString(reader.GetOrdinal("DstAddr"));

            SrcAS = reader.GetInt32(reader.GetOrdinal("SrcAS"));
            DstAS = reader.GetInt32(reader.GetOrdinal("DstAS"));
            EType = reader.GetInt32(reader.GetOrdinal("EType"));
            Proto = reader.GetInt32(reader.GetOrdinal("Proto"));
            SrcPort = reader.GetInt32(reader.GetOrdinal("SrcPort"));
            DstPort = reader.GetInt32(reader.GetOrdinal("DstPort"));
            Bytes = reader.GetInt32(reader.GetOrdinal("Bytes"));
            Packets = reader.GetInt32(reader.GetOrdinal("Packets"));
        }

        public FlowDTO() { }

        public FlowDTO(FlowsRaw w)
        {
            TimeReceived = w.Timereceived.Value;
            TimeFlowStart = w.Timeflowstart.Value;
            SequenceNum = w.Sequencenum.Value;
            SamplingRate = w.Samplingrate.Value;
            SamplerAddress = w.Sampleraddress;
            SrcAddr = w.Srcaddr;
            DstAddr = w.Dstaddr;
            SrcAS = w.Srcas.Value;
            DstAS = w.Dstas.Value;
            EType = w.Etype.Value;
            Proto = w.Proto.Value;
            SrcPort = w.Srcport.Value;
            DstPort = w.Dstport.Value;
            Bytes = w.Bytes.Value;
            Packets = w.Packets.Value;
    }

        public IEnumerator GetEnumerator()
        {
            yield return TimeReceived;
            yield return TimeFlowStart;
            yield return SequenceNum;
            yield return SamplingRate;
            yield return SamplerAddress;
            yield return SrcAddr;
            yield return DstAddr;
            yield return SrcAS;
            yield return DstAS;
            yield return EType;
            yield return Proto;
            yield return SrcPort;
            yield return DstPort;
            yield return Bytes;
            yield return Packets;
        }
    }

    public class FlowDTOMapper : IEntityMapper<FlowDTO>
    {
        public FlowDTO MapEntity(IDataReader reader)
        {
            return new FlowDTO(reader);
        }
    }
}
