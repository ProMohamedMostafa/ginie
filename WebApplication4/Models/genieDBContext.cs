using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GenieMistro.Models
{
    public partial class genieDBContext : IdentityDbContext<Auth.ApplicationUser>
    {
        public genieDBContext()
        {
        }

        public genieDBContext(DbContextOptions<genieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionPlan> ActionPlan { get; set; }
        public virtual DbSet<BusinessAccount> BusinessAccount { get; set; }
        public virtual DbSet<CompAssign> CompAssign { get; set; }
        public virtual DbSet<Competency> Competency { get; set; }
        public virtual DbSet<Indicator> Indicator { get; set; }
        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<MissionDept> MissionDept { get; set; }
        public virtual DbSet<MissionMissionDept> MissionMissionDept { get; set; }
        public virtual DbSet<Objective> Objective { get; set; }
        public virtual DbSet<ObjectiveEmployee> ObjectiveEmployee { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }
        public virtual DbSet<StratigicObjective> StratigicObjective { get; set; }
        public virtual DbSet<TbEmployee> TbEmployee { get; set; }
        public virtual DbSet<Vision> Vision { get; set; }

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
            base.OnModelCreating(modelBuilder);

            /*
            
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ActionPlan>(entity =>
            {
                entity.ToTable("ActionPlan");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.ActionPlans)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ActionPla__Objec__17F790F9");
            });

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

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.CompAssigns)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_compAssign_Competency");
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

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.ToTable("Mission");

                entity.Property(e => e.AffectingMisionSuccess).HasMaxLength(500);

                entity.Property(e => e.AffectingMissionFailure).HasMaxLength(500);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.MissionDescription).HasMaxLength(2000);

                entity.Property(e => e.MissionText)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MissionType).HasMaxLength(200);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.Vision)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.VisionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Mission__VisionI__74AE54BC");
            });

            modelBuilder.Entity<MissionDept>(entity =>
            {
                entity.HasIndex(e => e.DepName, "UQ__MissionD__49814543558B0F53")
                    .IsUnique();

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("depName");

                entity.Property(e => e.DeptDescription).HasMaxLength(500);
            });

            modelBuilder.Entity<MissionMissionDept>(entity =>
            {
                entity.HasKey(e => new { e.MissionId, e.MissionDeptId })
                    .HasName("PK__Mission___386573E9AE43507E");

                entity.ToTable("Mission_MissionDepts");

                entity.Property(e => e.MissionId).HasColumnName("MissionID");

                entity.Property(e => e.MissionDeptId).HasColumnName("MissionDeptID");

                entity.HasOne(d => d.MissionDept)
                    .WithMany(p => p.MissionMissionDepts)
                    .HasForeignKey(d => d.MissionDeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mission_M__Missi__04E4BC85");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionMissionDepts)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mission_M__Missi__03F0984C");
            });

            modelBuilder.Entity<Objective>(entity =>
            {
                entity.ToTable("Objective");

                entity.HasIndex(e => e.Name, "UQ__Objectiv__737584F69166717D")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Objectives)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Objective__proje__151B244E");
            });

            modelBuilder.Entity<ObjectiveEmployee>(entity =>
            {
                entity.HasKey(e => new { e.ObjectiveId, e.EmployeeId })
                    .HasName("PK__Objectiv__8BFB375CA1783A48");

                entity.ToTable("ObjectiveEmployee");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ObjectiveEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Objective__Emplo__1BC821DD");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.ObjectiveEmployees)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Objective__Objec__1AD3FDA4");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.StratigicObjective)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.StratigicObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Project__Stratig__114A936A");
            });

            modelBuilder.Entity<Purpose>(entity =>
            {
                entity.ToTable("Purpose");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PurposeDescription)
                    .HasMaxLength(2000)
                    .HasColumnName("purposeDescription");

                entity.Property(e => e.PurposeText).HasMaxLength(500);
            });

            modelBuilder.Entity<StratigicObjective>(entity =>
            {
                entity.ToTable("StratigicObjective");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.MissionId).HasColumnName("missionId");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StrObjDescription).HasMaxLength(500);

                entity.Property(e => e.StratigicObjectiveName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.DepdancyStratigicObjectiveNavigation)
                    .WithMany(p => p.InverseDepdancyStratigicObjectiveNavigation)
                    .HasForeignKey(d => d.DepdancyStratigicObjective)
                    .HasConstraintName("FK__Stratigic__Depda__01142BA1");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.StratigicObjectives)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StratigicObjective_Mission");
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

            modelBuilder.Entity<Vision>(entity =>
            {
                entity.ToTable("Vision");

                entity.Property(e => e.PurposeId).HasColumnName("purposeId");

                entity.Property(e => e.VisionDescription)
                    .HasMaxLength(2000)
                    .HasColumnName("visionDescription");

                entity.Property(e => e.VisionText)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("visionText");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.Visions)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Vision__purposeI__71D1E811");
            });

            OnModelCreatingPartial(modelBuilder);

            */
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
