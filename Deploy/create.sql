CREATE TABLE IF NOT EXISTS flows
(
    TimeReceived UInt64,
    TimeFlowStart UInt64,
    SequenceNum UInt32,
    SamplingRate UInt64,
    SamplerAddress FixedString(16),
    SrcAddr FixedString(16),
    DstAddr FixedString(16),
    SrcAS UInt32,
    DstAS UInt32,
    EType UInt32,
    Proto UInt32,
    SrcPort UInt32,
    DstPort UInt32,
    Bytes UInt64,
    Packets UInt64
)
ENGINE = Kafka
SETTINGS kafka_broker_list = '192.168.1.53:9092',
 kafka_topic_list = 'flows',
 kafka_group_name = 'clickhouse',
 kafka_format = 'Protobuf',
 kafka_schema = './flow.proto:FlowMessage';
 
CREATE TABLE  IF NOT EXISTS flows_5m
(
    `Date` Date,
    Timeslot DateTime,
    SrcAS UInt32,
    DstAS UInt32,
    `ETypeMap.EType` Array(UInt32),
    `ETypeMap.Bytes` Array(UInt64),
    `ETypeMap.Packets` Array(UInt64),
    `ETypeMap.Count` Array(UInt64),
    Bytes UInt64,
    Packets UInt64,
    `Count` UInt64
)
ENGINE = SummingMergeTree
PARTITION BY Date
ORDER BY (Date,
 Timeslot,
 SrcAS,
 DstAS,
 `ETypeMap.EType`)
SETTINGS index_granularity = 8192;

CREATE TABLE IF NOT EXISTS flows_raw
(
    Date Date,
    TimeReceived DateTime,
    TimeFlowStart DateTime,
    SequenceNum UInt32,
    SamplingRate UInt64,
    SamplerAddress FixedString(16),
    SrcAddr FixedString(16),
    DstAddr FixedString(16),
    SrcAS UInt32,
    DstAS UInt32,
    EType UInt32,
    Proto UInt32,
    SrcPort UInt32,
    DstPort UInt32,
    Bytes UInt64,
    Packets UInt64
)
ENGINE = MergeTree
PARTITION BY Date
ORDER BY TimeReceived
SETTINGS index_granularity = 8192;


CREATE MATERIALIZED VIEW IF NOT EXISTS default.flows_5m_view TO default.flows_5m
(
    `Date` Date,
    `Timeslot` DateTime('Europe/Moscow'),
    `SrcAS` UInt32,
    `DstAS` UInt32,
    `ETypeMap.EType` Array(UInt32),
    `ETypeMap.Bytes` Array(UInt64),
    `ETypeMap.Packets` Array(UInt64),
    `ETypeMap.Count` Array(UInt64),
    `Bytes` UInt64,
    `Packets` UInt64,
    `Count` UInt64
) AS
SELECT
    Date,
    toStartOfFiveMinute(TimeReceived) AS Timeslot,
    SrcAS,
    DstAS,
    [EType] AS `ETypeMap.EType`,
    [Bytes] AS `ETypeMap.Bytes`,
    [Packets] AS `ETypeMap.Packets`,
    [Count] AS `ETypeMap.Count`,
    sum(Bytes) AS Bytes,
    sum(Packets) AS Packets,
    count() AS Count
FROM default.flows_raw
GROUP BY
    Date,
    Timeslot,
    SrcAS,
    DstAS,
    `ETypeMap.EType`;

   
CREATE MATERIALIZED VIEW IF NOT EXISTS default.flows_raw_view TO default.flows_raw
(
    `Date` Date,
    `TimeReceived` UInt64,
    `TimeFlowStart` UInt64,
    `SequenceNum` UInt32,
    `SamplingRate` UInt64,
    `SamplerAddress` FixedString(16),
    `SrcAddr` FixedString(16),
    `DstAddr` FixedString(16),
    `SrcAS` UInt32,
    `DstAS` UInt32,
    `EType` UInt32,
    `Proto` UInt32,
    `SrcPort` UInt32,
    `DstPort` UInt32,
    `Bytes` UInt64,
    `Packets` UInt64
) AS
SELECT
    toDate(TimeReceived) AS Date, *
FROM default.flows;
    
    
CREATE TABLE IF NOT EXISTS data_sources (
                        Ip String,
                        OwnerUUID UUID,
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);
                        
CREATE TABLE IF NOT EXISTS data_source_types (
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);
                       
   CREATE TABLE IF NOT EXISTS data_destinations (
                        Ip String,
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);
                       
   CREATE TABLE IF NOT EXISTS data_destination_types (
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);
                       
   CREATE TABLE IF NOT EXISTS user_info (
                        Id UUID,
                        Name String,
                        Post String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Id);