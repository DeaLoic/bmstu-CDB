using AccessDB.DbModels.PostgreSQL;
using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DbModels.ClickHouse
{
    public class FlowClickHouse
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

        public FlowClickHouse(DateTime TimeReceived, DateTime TimeFlowStart, int SequenceNum,
                              int SamplingRate, string SamplerAddress, string SrcAddr,
                              string DstAddr, int SrcAS, int DstAS, int EType, int Proto,
                              int SrcPort, int DstPort, int Bytes, int Packets)
        {
            this.TimeReceived = TimeReceived;
            this.TimeFlowStart = TimeFlowStart;
            this.SequenceNum  = SequenceNum;
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

        public FlowClickHouse(IDataReader reader)
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

    public class FlowClickHouseMapperDB : IEntityMapper<FlowClickHouse>
    {
        public FlowClickHouse MapEntity(IDataReader reader)
        {
            return new FlowClickHouse(reader);
        }
    }
}
