﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";

        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtAddrct> GtAddrcts { get; set; } = null!;
        public virtual DbSet<GtAddrdt> GtAddrdts { get; set; } = null!;
        public virtual DbSet<GtAddrhd> GtAddrhds { get; set; } = null!;
        public virtual DbSet<GtAddrst> GtAddrsts { get; set; } = null!;
        public virtual DbSet<GtEadsfi> GtEadsfis { get; set; } = null!;
        public virtual DbSet<GtEaflin> GtEaflins { get; set; } = null!;
        public virtual DbSet<GtEcapcd> GtEcapcds { get; set; } = null!;
        public virtual DbSet<GtEcapct> GtEcapcts { get; set; } = null!;
        public virtual DbSet<GtEcbsen> GtEcbsens { get; set; } = null!;
        public virtual DbSet<GtEcbsln> GtEcbslns { get; set; } = null!;
        public virtual DbSet<GtEccncd> GtEccncds { get; set; } = null!;
        public virtual DbSet<GtEccnsd> GtEccnsds { get; set; } = null!;
        public virtual DbSet<GtEccuco> GtEccucos { get; set; } = null!;
        public virtual DbSet<GtEcpadr> GtEcpadrs { get; set; } = null!;
        public virtual DbSet<GtEcparm> GtEcparms { get; set; } = null!;
        public virtual DbSet<GtEcsupa> GtEcsupas { get; set; } = null!;
        public virtual DbSet<GtEscdst> GtEscdsts { get; set; } = null!;
        public virtual DbSet<GtEsclsl> GtEsclsls { get; set; } = null!;
        public virtual DbSet<GtEsclst> GtEsclsts { get; set; } = null!;
        public virtual DbSet<GtEscsst> GtEscssts { get; set; } = null!;
        public virtual DbSet<GtEsctsp> GtEsctsps { get; set; } = null!;
        public virtual DbSet<GtEsdoab> GtEsdoabs { get; set; } = null!;
        public virtual DbSet<GtEsdoad> GtEsdoads { get; set; } = null!;
        public virtual DbSet<GtEsdobl> GtEsdobls { get; set; } = null!;
        public virtual DbSet<GtEsdocd> GtEsdocds { get; set; } = null!;
        public virtual DbSet<GtEsdocl> GtEsdocls { get; set; } = null!;
        public virtual DbSet<GtEsdodx> GtEsdodxes { get; set; } = null!;
        public virtual DbSet<GtEsdoim> GtEsdoims { get; set; } = null!;
        public virtual DbSet<GtEsdold> GtEsdolds { get; set; } = null!;
        public virtual DbSet<GtEsdoro> GtEsdoros { get; set; } = null!;
        public virtual DbSet<GtEsdos1> GtEsdos1s { get; set; } = null!;
        public virtual DbSet<GtEsdos2> GtEsdos2s { get; set; } = null!;
        public virtual DbSet<GtEsdosc> GtEsdoscs { get; set; } = null!;
        public virtual DbSet<GtEsdosd> GtEsdosds { get; set; } = null!;
        public virtual DbSet<GtEsdosp> GtEsdosps { get; set; } = null!;
        public virtual DbSet<GtEsopcl> GtEsopcls { get; set; } = null!;
        public virtual DbSet<GtEspasm> GtEspasms { get; set; } = null!;
        public virtual DbSet<GtEsspbl> GtEsspbls { get; set; } = null!;
        public virtual DbSet<GtEsspcd> GtEsspcds { get; set; } = null!;
        public virtual DbSet<GtEsspcl> GtEsspcls { get; set; } = null!;
        public virtual DbSet<GtEsspct> GtEsspcts { get; set; } = null!;
        public virtual DbSet<GtEsspmc> GtEsspmcs { get; set; } = null!;
        public virtual DbSet<GtEssppa> GtEssppas { get; set; } = null!;
        public virtual DbSet<GtEsspun> GtEsspuns { get; set; } = null!;
        public virtual DbSet<GtEssrm> GtEssrms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtAddrct>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.StateCode, e.CityCode });

                entity.ToTable("GT_ADDRCT");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.CityDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtAddrdt>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.Zipcode, e.ZipserialNumber });

                entity.ToTable("GT_ADDRDT");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCode");

                entity.Property(e => e.ZipserialNumber).HasColumnName("ZIPSerialNumber");

                entity.Property(e => e.Area).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtAddrhd>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.StateCode, e.CityCode, e.Zipcode });

                entity.ToTable("GT_ADDRHD");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCode");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Zipdesc)
                    .HasMaxLength(75)
                    .HasColumnName("ZIPDesc");
            });

            modelBuilder.Entity<GtAddrst>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.StateCode });

                entity.ToTable("GT_ADDRST");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.StateDesc).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEadsfi>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.RoomNo });

                entity.ToTable("GT_EADSFI");

                entity.Property(e => e.RoomNo).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FloorId).HasColumnName("FloorID");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEaflin>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.RoomNo });

                entity.ToTable("GT_EAFLIN");

                entity.Property(e => e.RoomNo).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FloorId).HasColumnName("FloorID");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcapcd>(entity =>
            {
                entity.HasKey(e => e.ApplicationCode)
                    .HasName("PK_GT_ECAPCD_1");

                entity.ToTable("GT_ECAPCD");

                entity.Property(e => e.ApplicationCode).ValueGeneratedNever();

                entity.Property(e => e.CodeDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortCode).HasMaxLength(15);

                entity.HasOne(d => d.CodeTypeNavigation)
                    .WithMany(p => p.GtEcapcds)
                    .HasForeignKey(d => d.CodeType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECAPCD_GT_ECAPCT");
            });

            modelBuilder.Entity<GtEcapct>(entity =>
            {
                entity.HasKey(e => e.CodeType);

                entity.ToTable("GT_ECAPCT");

                entity.Property(e => e.CodeType).ValueGeneratedNever();

                entity.Property(e => e.CodeTyepDesc).HasMaxLength(50);

                entity.Property(e => e.CodeTypeControl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcbsen>(entity =>
            {
                entity.HasKey(e => e.BusinessId);

                entity.ToTable("GT_ECBSEN");

                entity.Property(e => e.BusinessId)
                    .ValueGeneratedNever()
                    .HasColumnName("BusinessID");

                entity.Property(e => e.BusinessDesc).HasMaxLength(75);

                entity.Property(e => e.BusinessUnitType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('S')")
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.LocationId });

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey, "IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationDescription).HasMaxLength(150);

                entity.Property(e => e.Lstatus).HasColumnName("LStatus");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortDesc).HasMaxLength(15);

                entity.Property(e => e.TocurrConversion).HasColumnName("TOCurrConversion");

                entity.Property(e => e.TolocalCurrency)
                    .IsRequired()
                    .HasColumnName("TOLocalCurrency")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TorealCurrency).HasColumnName("TORealCurrency");
            });

            modelBuilder.Entity<GtEccncd>(entity =>
            {
                entity.HasKey(e => e.Isdcode);

                entity.ToTable("GT_ECCNCD");

                entity.Property(e => e.Isdcode)
                    .ValueGeneratedNever()
                    .HasColumnName("ISDCode");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag).HasMaxLength(150);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.IsPinapplicable).HasColumnName("IsPINApplicable");

                entity.Property(e => e.IsPoboxApplicable).HasColumnName("IsPOBoxApplicable");

                entity.Property(e => e.MobileNumberPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.PincodePattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PINcodePattern");

                entity.Property(e => e.PoboxPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("POBoxPattern");

                entity.Property(e => e.ShortDateFormat)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GtEccnsd>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.StatutoryCode });

                entity.ToTable("GT_ECCNSD");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.StatPattern).HasMaxLength(25);

                entity.Property(e => e.StatShortCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatutoryDescription).HasMaxLength(50);

                entity.HasOne(d => d.IsdcodeNavigation)
                    .WithMany(p => p.GtEccnsds)
                    .HasForeignKey(d => d.Isdcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECCNSD_GT_ECCNCD");
            });

            modelBuilder.Entity<GtEccuco>(entity =>
            {
                entity.HasKey(e => e.CurrencyCode);

                entity.ToTable("GT_ECCUCO");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyName).HasMaxLength(25);

                entity.Property(e => e.DecimalPlaces).HasColumnType("decimal(6, 0)");

                entity.Property(e => e.DecimalPortionWord).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Symbol).HasMaxLength(10);
            });

            modelBuilder.Entity<GtEcpadr>(entity =>
            {
                entity.HasKey(e => new { e.DoctorCode, e.ParameterId });

                entity.ToTable("GT_ECPADR");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEcparm>(entity =>
            {
                entity.HasKey(e => new { e.ParameterType, e.ParameterId });

                entity.ToTable("GT_ECPARM");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParameterDesc).HasMaxLength(50);

                entity.Property(e => e.ParameterValueType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<GtEcsupa>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.StatutoryCode, e.ParameterId });

                entity.ToTable("GT_ECSUPA");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEscdst>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ServiceId, e.RateType, e.DoctorId, e.CurrencyCode, e.EffectiveDate })
                    .HasName("PK_GT_ESCDST_1");

                entity.ToTable("GT_ESCDST");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Tariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsclsl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ClinicId, e.ConsultationId, e.ServiceId });

                entity.ToTable("GT_ESCLSL");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsclst>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ClinicId, e.ConsultationId, e.ServiceId, e.RateType, e.CurrencyCode, e.EffectiveDate });

                entity.ToTable("GT_ESCLST");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Tariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEscsst>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ServiceId, e.RateType, e.SpecialtyId, e.CurrencyCode, e.EffectiveDate });

                entity.ToTable("GT_ESCSST");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Tariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsctsp>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.ClinicId, e.ConsultationId, e.ServiceId, e.RateType, e.CurrencyCode, e.EffectiveDate });

                entity.ToTable("GT_ESCTSP");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Tariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsdoab>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("GT_ESDOAB");

                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoctorID");

                entity.Property(e => e.CertificationCourse).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DoctorRemarks).HasMaxLength(250);

                entity.Property(e => e.Experience).HasMaxLength(250);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.LanguageKnown).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdoad>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.DoctorId, e.Isdcode });

                entity.ToTable("GT_ESDOAD");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCode");

                entity.Property(e => e.ZipserialNumber).HasColumnName("ZIPSerialNumber");
            });

            modelBuilder.Entity<GtEsdobl>(entity =>
            {
                entity.HasKey(e => new { e.DoctorId, e.BusinessKey });

                entity.ToTable("GT_ESDOBL");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdocd>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("GT_ESDOCD");

                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DoctorName).HasMaxLength(50);

                entity.Property(e => e.DoctorRegnNo).HasMaxLength(25);

                entity.Property(e => e.DoctorShortName).HasMaxLength(10);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.TraiffFrom)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<GtEsdocl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.DoctorId, e.ClinicId })
                    .HasName("PK_GT_ESDOCL_1");

                entity.ToTable("GT_ESDOCL");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdodx>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("GT_ESDODX");

                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DoxyMeUrl)
                    .HasMaxLength(250)
                    .HasColumnName("DoxyMeURL");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdoim>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("GT_ESDOIM");

                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DoctorImage).HasColumnType("image");

                entity.Property(e => e.DoctorSignature).HasColumnType("image");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdold>(entity =>
            {
                entity.HasKey(e => new { e.DoctorId, e.OnLeaveFrom, e.OnLeaveTill });

                entity.ToTable("GT_ESDOLD");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.OnLeaveFrom).HasColumnType("datetime");

                entity.Property(e => e.OnLeaveTill).HasColumnType("datetime");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.GtEsdolds)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOLD_GT_ESDOCD");
            });

            modelBuilder.Entity<GtEsdoro>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ClinicId, e.ConsultationId, e.ServiceId, e.DoctorId, e.RateType, e.CurrencyCode, e.EffectiveDate });

                entity.ToTable("GT_ESDORO");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Tariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsdos1>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ConsultationId, e.ClinicId, e.SpecialtyId, e.DoctorId, e.DayOfWeek, e.SerialNo });

                entity.ToTable("GT_ESDOS1");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DayOfWeek).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.RoomNo).HasMaxLength(10);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.GtEsdos1s)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS1_GT_ESDOCD");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.GtEsdos1s)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS1_GT_ESSPCD");
            });

            modelBuilder.Entity<GtEsdos2>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ConsultationId, e.ClinicId, e.SpecialtyId, e.DoctorId, e.DayOfWeek, e.SerialNo })
                    .HasName("PK_GT_ESDOS2_1");

                entity.ToTable("GT_ESDOS2");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DayOfWeek).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Xlreference)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("XLReference");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.GtEsdos2s)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS2_GT_ESDOCD");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.GtEsdos2s)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS2_GT_ESSPCD");
            });

            modelBuilder.Entity<GtEsdosc>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ConsultationId, e.ClinicId, e.SpecialtyId, e.DoctorId, e.ScheduleChangeDate });

                entity.ToTable("GT_ESDOSC");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.ScheduleChangeDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.GtEsdoscs)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOSC_GT_ESDOCD");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.GtEsdoscs)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOSC_GT_ESSPCD");
            });

            modelBuilder.Entity<GtEsdosd>(entity =>
            {
                entity.HasKey(e => new { e.DoctorId, e.Isdcode, e.StatutoryCode, e.EffectiveFrom })
                    .HasName("PK_GT_ESDOSD_1");

                entity.ToTable("GT_ESDOSD");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .HasColumnName("FormID")
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.StatutoryDescription).HasMaxLength(50);

                entity.Property(e => e.TaxPerc).HasColumnType("numeric(5, 2)");
            });

            modelBuilder.Entity<GtEsdosp>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.DoctorId })
                    .HasName("PK_GT_ESDOSP_1");

                entity.ToTable("GT_ESDOSP");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsopcl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ClinicId, e.ConsultationId });

                entity.ToTable("GT_ESOPCL");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEspasm>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ServiceId, e.ParameterId });

                entity.ToTable("GT_ESPASM");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.GtEspasms)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESPASM_GT_ESSRMS");
            });

            modelBuilder.Entity<GtEsspbl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId })
                    .HasName("PK_GT_ESSPBL_1");

                entity.ToTable("GT_ESSPBL");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsspcd>(entity =>
            {
                entity.HasKey(e => e.SpecialtyId);

                entity.ToTable("GT_ESSPCD");

                entity.Property(e => e.SpecialtyId)
                    .ValueGeneratedNever()
                    .HasColumnName("SpecialtyID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FocusArea).HasMaxLength(2000);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MedicalIcon).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.SpecialtyDesc).HasMaxLength(50);

                entity.Property(e => e.SpecialtyGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SpecialtyType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<GtEsspcl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.DayOfWeek });

                entity.ToTable("GT_ESSPCL");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Ip).HasColumnName("IP");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Op).HasColumnName("OP");

                entity.Property(e => e.Ot).HasColumnName("OT");
            });

            modelBuilder.Entity<GtEsspct>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.ClinicId, e.RateType, e.ClinicType, e.EffectiveFrom });

                entity.ToTable("GT_ESSPCT");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.ConsultationTariff).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.RevisitConsultationTariff).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SecRevisitConsultationTariff).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ServiceRule)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TeleConsultationTariff).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsspmc>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId })
                    .HasName("PK_GT_ESSPMC_1");

                entity.ToTable("GT_ESSPMC");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEssppa>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.ParameterId });

                entity.ToTable("GT_ESSPPA");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEsspun>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.EffectiveFrom });

                entity.ToTable("GT_ESSPUN");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.EffectiveTill).HasColumnType("datetime");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEssrm>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("GT_ESSRMS");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InternalServiceCode).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ServiceClassId).HasColumnName("ServiceClassID");

                entity.Property(e => e.ServiceDesc).HasMaxLength(75);

                entity.Property(e => e.ServiceShortDesc)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}