﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectBaseVue_Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectBaseVueEntities : DbContext
    {
        public ProjectBaseVueEntities()
            : base("name=ProjectBaseVueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<APISetting> APISetting { get; set; }
        public virtual DbSet<Changelog> Changelog { get; set; }
        public virtual DbSet<ChangelogDetail> ChangelogDetail { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyDetail> CompanyDetail { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Example> Example { get; set; }
        public virtual DbSet<ExampleDetail> ExampleDetail { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupMenu> GroupMenu { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<NotificationLog> NotificationLog { get; set; }
        public virtual DbSet<RequestLog> RequestLog { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RunningNumber> RunningNumber { get; set; }
        public virtual DbSet<RunningNumberDate> RunningNumberDate { get; set; }
        public virtual DbSet<RunningNumberMonth> RunningNumberMonth { get; set; }
        public virtual DbSet<RunningNumberYear> RunningNumberYear { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserPassLog> UserPassLog { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
    }
}