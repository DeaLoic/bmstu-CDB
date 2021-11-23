using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class defaultContext : DbContext
    {
        public defaultContext()
        {
        }

        public defaultContext(DbContextOptions<defaultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataDestination> DataDestinations { get; set; }
        public virtual DbSet<DataDestinationType> DataDestinationTypes { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<DataSourceType> DataSourceTypes { get; set; }
        public virtual DbSet<FlowsRaw> FlowsRaws { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=default;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<DataDestination>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("data_destinations");

                entity.Property(e => e.Ip)
                    .HasColumnType("character varying")
                    .HasColumnName("ip");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("data_destinations_type_fkey");
            });

            modelBuilder.Entity<DataDestinationType>(entity =>
            {
                entity.HasKey(e => e.Type)
                    .HasName("data_destination_types_pkey");

                entity.ToTable("data_destination_types");

                entity.Property(e => e.Type)
                    .ValueGeneratedNever()
                    .HasColumnName("type");

                entity.Property(e => e.Info)
                    .HasColumnType("character varying")
                    .HasColumnName("info");
            });

            modelBuilder.Entity<DataSource>(entity =>
            {
                entity.HasKey(e => e.Ip)
                    .HasName("data_sources_pkey");

                entity.ToTable("data_sources");

                entity.Property(e => e.Ip)
                    .HasColumnType("character varying")
                    .HasColumnName("ip");

                entity.Property(e => e.Owneruuid).HasColumnName("owneruuid");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Owneruu)
                    .WithMany(p => p.DataSources)
                    .HasForeignKey(d => d.Owneruuid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("data_sources_owneruuid_fkey");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.DataSources)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("data_sources_type_fkey");
            });

            modelBuilder.Entity<DataSourceType>(entity =>
            {
                entity.HasKey(e => e.Type)
                    .HasName("data_source_types_pkey");

                entity.ToTable("data_source_types");

                entity.Property(e => e.Type)
                    .ValueGeneratedNever()
                    .HasColumnName("type");

                entity.Property(e => e.Info)
                    .HasColumnType("character varying")
                    .HasColumnName("info");
            });

            modelBuilder.Entity<FlowsRaw>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flows_raw");

                entity.Property(e => e.Bytes).HasColumnName("bytes");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Dstaddr)
                    .HasMaxLength(16)
                    .HasColumnName("dstaddr")
                    .IsFixedLength(true);

                entity.Property(e => e.Dstas).HasColumnName("dstas");

                entity.Property(e => e.Dstport).HasColumnName("dstport");

                entity.Property(e => e.Etype).HasColumnName("etype");

                entity.Property(e => e.Packets).HasColumnName("packets");

                entity.Property(e => e.Proto).HasColumnName("proto");

                entity.Property(e => e.Sampleraddress)
                    .HasMaxLength(16)
                    .HasColumnName("sampleraddress")
                    .IsFixedLength(true);

                entity.Property(e => e.Samplingrate).HasColumnName("samplingrate");

                entity.Property(e => e.Sequencenum).HasColumnName("sequencenum");

                entity.Property(e => e.Srcaddr)
                    .HasMaxLength(16)
                    .HasColumnName("srcaddr")
                    .IsFixedLength(true);

                entity.Property(e => e.Srcas).HasColumnName("srcas");

                entity.Property(e => e.Srcport).HasColumnName("srcport");

                entity.Property(e => e.Timeflowstart).HasColumnName("timeflowstart");

                entity.Property(e => e.Timereceived).HasColumnName("timereceived");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("user_info");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Post)
                    .HasColumnType("character varying")
                    .HasColumnName("post");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
