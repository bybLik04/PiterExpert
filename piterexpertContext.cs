using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PiterExp
{
    public partial class piterexpertContext : DbContext
    {
        public piterexpertContext()
        {
        }

        public piterexpertContext(DbContextOptions<piterexpertContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Договор> Договорs { get; set; }
        public virtual DbSet<ДоговорУслуга> ДоговорУслугаs { get; set; }
        public virtual DbSet<Клиенты> Клиентыs { get; set; }
        public virtual DbSet<КлиентыЛьготы> КлиентыЛьготыs { get; set; }
        public virtual DbSet<Льготы> Льготыs { get; set; }
        public virtual DbSet<СистемаНалогооблаженияКлиент> СистемаНалогооблаженияКлиентs { get; set; }
        public virtual DbSet<СистемаНалогообложения> СистемаНалогообложенияs { get; set; }
        public virtual DbSet<Сотрудники> Сотрудникиs { get; set; }
        public virtual DbSet<Услуги> Услугиs { get; set; }
        public virtual DbSet<УслугиВэд> УслугиВэдs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;persist security info=False;user=root;password=1234;database=piterexpert", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Договор>(entity =>
            {
                entity.HasKey(e => e.НомерДоговора)
                    .HasName("PRIMARY");

                entity.ToTable("договор");

                entity.HasIndex(e => e.IdСотрудника, "ff2_idx");

                entity.HasIndex(e => e.IdКлиента, "fk10");

                entity.HasIndex(e => e.НомерДоговора, "Номер договора_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.НомерДоговора)
                    .ValueGeneratedNever()
                    .HasColumnName("Номер договора");

                entity.Property(e => e.IdКлиента).HasColumnName("idКлиента");

                entity.Property(e => e.IdСотрудника).HasColumnName("idСотрудника");

                entity.Property(e => e.ДатаВыполненияДоговора)
                    .HasColumnType("date")
                    .HasColumnName("Дата выполнения договора");

                entity.Property(e => e.ДатаДоговора)
                    .HasColumnType("date")
                    .HasColumnName("Дата договора");

                entity.Property(e => e.Сумма).HasPrecision(10);

                entity.HasOne(d => d.IdКлиентаNavigation)
                    .WithMany(p => p.Договорs)
                    .HasForeignKey(d => d.IdКлиента)
                    .HasConstraintName("fk10");

                entity.HasOne(d => d.IdСотрудникаNavigation)
                    .WithMany(p => p.Договорs)
                    .HasForeignKey(d => d.IdСотрудника)
                    .HasConstraintName("ff2");
            });

            modelBuilder.Entity<ДоговорУслуга>(entity =>
            {
                entity.HasKey(e => new { e.IdДоговора, e.IdУслуга })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("договор-услуга");

                entity.HasIndex(e => e.IdУслуга, "услуг_idx");

                entity.Property(e => e.IdДоговора).HasColumnName("idДоговора");

                entity.Property(e => e.IdУслуга).HasColumnName("idУслуга");

                entity.HasOne(d => d.IdДоговораNavigation)
                    .WithMany(p => p.ДоговорУслугаs)
                    .HasForeignKey(d => d.IdДоговора)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("дог");

                entity.HasOne(d => d.IdУслугаNavigation)
                    .WithMany(p => p.ДоговорУслугаs)
                    .HasForeignKey(d => d.IdУслуга)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("услуг");
            });

            modelBuilder.Entity<Клиенты>(entity =>
            {
                entity.HasKey(e => e.IdКлиента)
                    .HasName("PRIMARY");

                entity.ToTable("клиенты");

                entity.Property(e => e.IdКлиента).HasColumnName("idКлиента");

                entity.Property(e => e.Адрес)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Инн).HasColumnName("ИНН");

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<КлиентыЛьготы>(entity =>
            {
                entity.HasKey(e => new { e.IdКлиента, e.IdЛьготы })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("клиенты-льготы");

                entity.HasIndex(e => e.IdЛьготы, "льг_idx");

                entity.Property(e => e.IdКлиента).HasColumnName("idКлиента");

                entity.Property(e => e.IdЛьготы).HasColumnName("idЛьготы");

                entity.HasOne(d => d.IdКлиентаNavigation)
                    .WithMany(p => p.КлиентыЛьготыs)
                    .HasForeignKey(d => d.IdКлиента)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("клие");

                entity.HasOne(d => d.IdЛьготыNavigation)
                    .WithMany(p => p.КлиентыЛьготыs)
                    .HasForeignKey(d => d.IdЛьготы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("льг");
            });

            modelBuilder.Entity<Льготы>(entity =>
            {
                entity.HasKey(e => e.IdЛьготы)
                    .HasName("PRIMARY");

                entity.ToTable("льготы");

                entity.Property(e => e.IdЛьготы)
                    .ValueGeneratedNever()
                    .HasColumnName("idЛьготы");

                entity.Property(e => e.НазваниеЛьготы)
                    .HasMaxLength(45)
                    .HasColumnName("Название Льготы");
            });

            modelBuilder.Entity<СистемаНалогооблаженияКлиент>(entity =>
            {
                entity.HasKey(e => new { e.IdСистемы, e.IdКлиента })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("система_налогооблажения-клиент");

                entity.HasIndex(e => e.IdКлиента, "кл_idx");

                entity.Property(e => e.IdСистемы).HasColumnName("idСистемы");

                entity.Property(e => e.IdКлиента).HasColumnName("idКлиента");

                entity.HasOne(d => d.IdКлиентаNavigation)
                    .WithMany(p => p.СистемаНалогооблаженияКлиентs)
                    .HasForeignKey(d => d.IdКлиента)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("кл");

                entity.HasOne(d => d.IdСистемыNavigation)
                    .WithMany(p => p.СистемаНалогооблаженияКлиентs)
                    .HasForeignKey(d => d.IdСистемы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("сис");
            });

            modelBuilder.Entity<СистемаНалогообложения>(entity =>
            {
                entity.HasKey(e => e.IdСистемы)
                    .HasName("PRIMARY");

                entity.ToTable("система_налогообложения");

                entity.Property(e => e.IdСистемы)
                    .ValueGeneratedNever()
                    .HasColumnName("idСистемы");

                entity.Property(e => e.НазваниеСистемы)
                    .HasMaxLength(45)
                    .HasColumnName("Название системы");
            });

            modelBuilder.Entity<Сотрудники>(entity =>
            {
                entity.HasKey(e => e.IdСотрудника)
                    .HasName("PRIMARY");

                entity.ToTable("сотрудники");

                entity.Property(e => e.IdСотрудника)
                    .ValueGeneratedNever()
                    .HasColumnName("idСотрудника");

                entity.Property(e => e.Имя)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Отчество)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Фамилия)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Услуги>(entity =>
            {
                entity.HasKey(e => e.IdУслуги)
                    .HasName("PRIMARY");

                entity.ToTable("услуги");

                entity.Property(e => e.IdУслуги)
                    .ValueGeneratedNever()
                    .HasColumnName("idУслуги");

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<УслугиВэд>(entity =>
            {
                entity.ToTable("услуги вэд");

                entity.HasIndex(e => e.IdДоговора, "fk9_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdДоговора).HasColumnName("idДоговора");

                entity.Property(e => e.ИмпортЕаэс).HasColumnName("импорт ЕАЭС");

                entity.Property(e => e.ИмпортКромеЕаэс).HasColumnName("импорт кроме ЕАЭС");

                entity.Property(e => e.СтатотчетностьВФтсПоЕаэс).HasColumnName("статотчетность в ФТС по ЕАЭС");

                entity.Property(e => e.УслугиВэд1).HasColumnName("услуги вэд");

                entity.Property(e => e.ЭкспортЕаэс).HasColumnName("экспорт ЕАЭС");

                entity.Property(e => e.ЭкспортКромеЕаэс).HasColumnName("экспорт кроме ЕАЭС");

                entity.HasOne(d => d.IdДоговораNavigation)
                    .WithMany(p => p.УслугиВэдs)
                    .HasForeignKey(d => d.IdДоговора)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
