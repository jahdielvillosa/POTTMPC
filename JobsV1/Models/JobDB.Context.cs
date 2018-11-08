﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobsV1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JobDBContainer : DbContext
    {
        public JobDBContainer()
            : base("name=JobDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<JobMain> JobMains { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<JobServices> JobServices { get; set; }
        public virtual DbSet<JobItinerary> JobItineraries { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<JobPickup> JobPickups { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<SupplierType> SupplierTypes { get; set; }
        public virtual DbSet<SupplierItem> SupplierItems { get; set; }
        public virtual DbSet<JobServicePickup> JobServicePickups { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<JobThru> JobThrus { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<JobPayment> JobPayments { get; set; }
        public virtual DbSet<CarCategory> CarCategories { get; set; }
        public virtual DbSet<CarUnit> CarUnits { get; set; }
        public virtual DbSet<CarDestination> CarDestinations { get; set; }
        public virtual DbSet<CarRate> CarRates { get; set; }
        public virtual DbSet<CarReservation> CarReservations { get; set; }
        public virtual DbSet<CarImage> CarImages { get; set; }
        public virtual DbSet<JobContact> JobContacts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<ProductImage> ProductImages1 { get; set; }
        public virtual DbSet<ProductCondition> ProductConditions { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductProdCat> ProductProdCats { get; set; }
        public virtual DbSet<PreDefinedNote> PreDefinedNotes { get; set; }
        public virtual DbSet<JobNote> JobNotes { get; set; }
        public virtual DbSet<JobChecklist> JobChecklists { get; set; }
        public virtual DbSet<CustCategory> CustCategories { get; set; }
        public virtual DbSet<CustCat> CustCats { get; set; }
        public virtual DbSet<CustEntMain> CustEntMains { get; set; }
        public virtual DbSet<SalesLead> SalesLeads { get; set; }
        public virtual DbSet<SalesStatusCode> SalesStatusCodes { get; set; }
        public virtual DbSet<SalesStatus> SalesStatus { get; set; }
        public virtual DbSet<SalesActCode> SalesActCodes { get; set; }
        public virtual DbSet<SalesActivity> SalesActivities { get; set; }
        public virtual DbSet<CustEntity> CustEntities { get; set; }
        public virtual DbSet<SalesActStatus> SalesActStatus { get; set; }
        public virtual DbSet<SalesLeadCatCode> SalesLeadCatCodes { get; set; }
        public virtual DbSet<SalesLeadCategory> SalesLeadCategories { get; set; }
        public virtual DbSet<CustSalesCategory> CustSalesCategories { get; set; }
        public virtual DbSet<SrvActionCode> SrvActionCodes { get; set; }
        public virtual DbSet<SrvActionItem> SrvActionItems { get; set; }
        public virtual DbSet<JobAction> JobActions { get; set; }
        public virtual DbSet<InvItem> InvItems { get; set; }
        public virtual DbSet<InvItemCat> InvItemCats { get; set; }
        public virtual DbSet<InvItemCategory> InvItemCategories { get; set; }
        public virtual DbSet<JobServiceItem> JobServiceItems { get; set; }
        public virtual DbSet<SupplierInvItem> SupplierInvItems { get; set; }
        public virtual DbSet<JobNotificationRequest> JobNotificationRequests { get; set; }
        public virtual DbSet<CustFiles> CustFiles { get; set; }
        public virtual DbSet<SupplierPoHdr> SupplierPoHdrs { get; set; }
        public virtual DbSet<SupplierPoDtl> SupplierPoDtls { get; set; }
        public virtual DbSet<SupplierPoStatus> SupplierPoStatus { get; set; }
        public virtual DbSet<SupplierPoItem> SupplierPoItems { get; set; }
        public virtual DbSet<CustFileRef> CustFileRefs { get; set; }
        public virtual DbSet<SalesLeadLink> SalesLeadLinks { get; set; }
        public virtual DbSet<InvCarRecord> InvCarRecords { get; set; }
        public virtual DbSet<InvCarRecordType> InvCarRecordTypes { get; set; }
        public virtual DbSet<InvCarGateControl> InvCarGateControls { get; set; }
        public virtual DbSet<JobTrail> JobTrails { get; set; }
        public virtual DbSet<CarViewPage> CarViewPages { get; set; }
        public virtual DbSet<CarRatePackage> CarRatePackages { get; set; }
        public virtual DbSet<CarRateUnitPackage> CarRateUnitPackages { get; set; }
        public virtual DbSet<CarResPackage> CarResPackages { get; set; }
        public virtual DbSet<CarUnitMeta> CarUnitMetas { get; set; }
    }
}