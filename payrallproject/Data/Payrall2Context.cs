using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using payrallproject.Models.Domains.Temp;

namespace payrallproject.Data;

public partial class Payrall2Context : DbContext
{
    public Payrall2Context(DbContextOptions<Payrall2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }

    public virtual DbSet<EmployeeOvertime> EmployeeOvertimes { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<JobRole> JobRoles { get; set; }

    public virtual DbSet<Leaf> Leaves { get; set; }

    public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }

    public virtual DbSet<Leaves2> Leaves2s { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Loanrepayment> Loanrepayments { get; set; }

    public virtual DbSet<NoPayDay> NoPayDays { get; set; }

    public virtual DbSet<NoPayEntry> NoPayEntries { get; set; }

    public virtual DbSet<Ot> Ots { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalaryReport> SalaryReports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.EmployeeCategoriesId, "IX_Departments_EmployeeCategoriesId");

            entity.HasOne(d => d.EmployeeCategories).WithMany(p => p.Departments).HasForeignKey(d => d.EmployeeCategoriesId);
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.ToTable("Employe");

            entity.HasIndex(e => e.DepartmentId, "IX_Employe_DepartmentID");

            entity.HasIndex(e => e.EmployeeCategoriesId, "IX_Employe_EmployeeCategoriesID");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeeCategoriesId).HasColumnName("EmployeeCategoriesID");

            entity.HasOne(d => d.Department).WithMany(p => p.Employes).HasForeignKey(d => d.DepartmentId);

            entity.HasOne(d => d.EmployeeCategories).WithMany(p => p.Employes).HasForeignKey(d => d.EmployeeCategoriesId);

            entity.HasOne(d => d.JobRole).WithMany(p => p.Employes)
                .HasForeignKey(d => d.JobRoleId)
                .HasConstraintName("FK_Employe_JobRoles");
        });

        modelBuilder.Entity<EmployeeOvertime>(entity =>
        {
            entity.HasIndex(e => e.EmployeId, "IX_EmployeeOvertimes_EmployeId");

            entity.HasIndex(e => e.OtId, "IX_EmployeeOvertimes_OtId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employe).WithMany(p => p.EmployeeOvertimes).HasForeignKey(d => d.EmployeId);

            entity.HasOne(d => d.Ot).WithMany(p => p.EmployeeOvertimes).HasForeignKey(d => d.OtId);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.ToTable("Holiday");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.HolidayType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<JobRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobRoles__3214EC07C2B4D26E");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleName).HasMaxLength(255);

            entity.HasOne(d => d.Department).WithMany(p => p.JobRoles)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_JobRoles_Departments");

            entity.HasOne(d => d.EmployeeCategories).WithMany(p => p.JobRoles)
                .HasForeignKey(d => d.EmployeeCategoriesId)
                .HasConstraintName("FK_JobRoles_EmployeeCategories");
        });

        modelBuilder.Entity<Leaf>(entity =>
        {
            entity.HasIndex(e => e.EmployeId, "IX_Leaves_EmployeID");

            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");

            entity.HasOne(d => d.Employe).WithMany(p => p.Leaves).HasForeignKey(d => d.EmployeId);
        });

        modelBuilder.Entity<LeaveBalance>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_LeaveBalances_EmployeeId");

            entity.Property(e => e.BalanceDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EntitledDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LeaveType).HasMaxLength(20);
            entity.Property(e => e.UsedDays).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveBalances).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Leaves2>(entity =>
        {
            entity.ToTable("Leaves2");

            entity.HasIndex(e => e.EmployeeId, "IX_Leaves2_EmployeeId");

            entity.Property(e => e.LeaveType).HasMaxLength(20);
            entity.Property(e => e.NumberOfDays).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Employee).WithMany(p => p.Leaves2s).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasIndex(e => e.EmployeId, "IX_Loans_EmployeID");

            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");
            entity.Property(e => e.MonthlyInstallment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RemainingBalance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employe).WithMany(p => p.Loans).HasForeignKey(d => d.EmployeId);
        });

        modelBuilder.Entity<Loanrepayment>(entity =>
        {
            entity.ToTable("Loanrepayment");

            entity.HasIndex(e => e.LoanId, "IX_Loanrepayment_LoanId");

            entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RemainingBalance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Loan).WithMany(p => p.Loanrepayments).HasForeignKey(d => d.LoanId);
        });

        modelBuilder.Entity<NoPayDay>(entity =>
        {
            entity.ToTable("NoPayDay");

            entity.HasIndex(e => e.EmployeId, "IX_NoPayDay_EmployeID");

            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");

            entity.HasOne(d => d.Employe).WithMany(p => p.NoPayDays).HasForeignKey(d => d.EmployeId);
        });

        modelBuilder.Entity<NoPayEntry>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_NoPayEntries_EmployeeId");

            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithMany(p => p.NoPayEntries).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Ot>(entity =>
        {
            entity.ToTable("OT");

            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_PasswordResetTokens_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResetTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SalaryReport>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_SalaryReports_EmployeeId");

            entity.Property(e => e.AttendanceAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BasicSala).HasColumnName("basicSala");
            entity.Property(e => e.Bonus).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeContribution).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Epf1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Epf2).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EpfLiableSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Etf).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FoodAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Incentives).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InternetAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KpiAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Loans).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MedicalAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NoPay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ot1Hours).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ot1Payment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ot2Hours).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ot2Payment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OtherDeductions).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalaryAdvances).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalDeductions).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalOtPayment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransportAllowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Wages).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryReports).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasIndex(e => e.RolesId, "IX_UserRoles_RolesId");

            entity.HasIndex(e => e.UserId, "IX_UserRoles_UserId");

            entity.HasOne(d => d.Roles).WithMany(p => p.UserRoles).HasForeignKey(d => d.RolesId);

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
