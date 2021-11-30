CREATE DATABASE IF NOT EXISTS default;

CREATE TABLE IF NOT EXISTS default.flows
    (
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
    )
    ENGINE = Kafka()
    SETTINGS kafka_broker_list = 'kafka:9092',
        kafka_topic_list = 'flows',
        kafka_group_name = 'clickhouse',
        kafka_format = 'Protobuf',
        kafka_schema = './flow.proto:FlowMessage',
        kafka_skip_broken_messages=65535;
 
CREATE TABLE IF NOT EXISTS default.flows_raw
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
    toDate(TimeReceived) AS Date,    
 	`TimeReceived`,
    `TimeFlowStart`,
    `SequenceNum`,
    `SamplingRate`,

    unhex(replaceRegexpOne(hex(SamplerAddress),
 '(((\\d|[A-F]){8}).*)',
 '\\200000000')) as SamplerAddress,

    unhex(replaceRegexpOne(hex(SrcAddr),
 '(((\\d|[A-F]){8}).*)',
 '\\200000000')) AS SrcAddr,

    unhex(replaceRegexpOne(hex(DstAddr),
 '(((\\d|[A-F]){8}).*)',
 '\\200000000')) AS DstAddr,
    `SrcAS`,
    `DstAS`,
    `EType`,
    `Proto`,
    `SrcPort`,
    `DstPort`,
    `Bytes`,
    `Packets`

FROM default.flows;
    
    
CREATE TABLE IF NOT EXISTS default.data_sources (
                        Ip String,
                        OwnerUUID UUID,
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);
                        
CREATE TABLE IF NOT EXISTS default.data_source_types (
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);
                       
CREATE TABLE IF NOT EXISTS default.data_destinations (
                        Ip String,
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);
                       
CREATE TABLE IF NOT EXISTS default.data_destination_types (
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);
                       
CREATE TABLE IF NOT EXISTS default.user_info (
                        Id UUID,
                        Name String,
                        Post String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Id);