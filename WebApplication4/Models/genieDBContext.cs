using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GenieMistro.Models
{
    public partial class genieDBContext : DbContext
    {
        public genieDBContext()
        {
        }

        public genieDBContext(DbContextOptions<genieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusinessAccount> BusinessAccounts { get; set; }
        public virtual DbSet<CompAssign> CompAssigns { get; set; }
        public virtual DbSet<Competency> Competencies { get; set; }
        public virtual DbSet<Indicator> Indicators { get; set; }
        public virtual DbSet<TbEmployee> TbEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=genieDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BusinessAccount>(entity =>
            {
                entity.ToTable("BusinessAccount");

                entity.HasIndex(e => e.CompanyName, "UQ__Business__9BCE05DC6CD7D758")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Business__A9D105349AD036B1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BPhone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("bPhone");

                entity.Property(e => e.BaPassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("baPassword");

                entity.Property(e => e.BaWebSite)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("baWebSite");

                entity.Property(e => e.BusinessDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CompAssign>(entity =>
            {
                entity.ToTable("compAssign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComLevel).HasColumnName("comLevel");

                entity.Property(e => e.CompId).HasColumnName("compId");
            });

            modelBuilder.Entity<Competency>(entity =>
            {
                entity.HasKey(e => e.ComId)
                    .HasName("PK__Competen__9052B55633F749F8");

                entity.ToTable("Competency");

                entity.Property(e => e.ComId).HasColumnName("comId");

                entity.Property(e => e.ComDeptName)
                    .HasMaxLength(100)
                    .HasColumnName("comDeptName");

                entity.Property(e => e.ComLevels).HasColumnName("comLevels");

                entity.Property(e => e.ComType)
                    .HasMaxLength(100)
                    .HasColumnName("comType");
            });

            modelBuilder.Entity<Indicator>(entity =>
            {
                entity.ToTable("Indicator");

                entity.Property(e => e.ComId).HasColumnName("comId");

                entity.Property(e => e.IndicatorText).HasMaxLength(300);

                entity.HasOne(d => d.Com)
                    .WithMany(p => p.Indicators)
                    .HasForeignKey(d => d.ComId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Indicator__comId__38996AB5");
            });

            modelBuilder.Entity<TbEmployee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__tbEmploy__AFB3EC0D15838026");

                entity.ToTable("tbEmployee");

                entity.Property(e => e.EmpId).HasColumnName("empId");

                entity.Property(e => e.EmpEmail)
                    .HasMaxLength(100)
                    .HasColumnName("empEmail");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(100)
                    .HasColumnName("empName");

                entity.Property(e => e.EmpPassword)
                    .HasMaxLength(100)
                    .HasColumnName("empPassword");

                entity.Property(e => e.EmpTitle)
                    .HasMaxLength(100)
                    .HasColumnName("empTitle");

                entity.Property(e => e.ManagerId).HasColumnName("managerID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
