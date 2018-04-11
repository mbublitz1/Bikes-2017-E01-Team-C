using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

#region Additional Namespaces
using eBike.Data.Entities;
#endregion

namespace eBike.System.DAL
{
    internal partial class eBikesContext : DbContext
    {
        public eBikesContext() : base("Name=eBikesDB")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<JobDetailPart> JobDetailParts { get; set; }
        public DbSet<JobDetail> JobDetails { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<OnlineCustomer> OnlineCustomer { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<ReceiveOrderDetail> ReceiveOrderDetails { get; set; }
        public DbSet<ReceiveOrder> ReceiveOrders { get; set; }
        public DbSet<ReturnedOrderDetail> ReturnedOrderDetails { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleRefundDetail> SaleRefundDetails { get; set; }
        public DbSet<SaleRefund> SaleRefunds { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<StandardJobPart> StandardJobParts { get; set; }
        public DbSet<StandardJob> StandardJobs { get; set; }
        public DbSet<UnorderedPurchaseItemCart> UnorderedPurchaseItemCarts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Added to deal with problem that entity framework is pluralizing table name when creating sql query
            modelBuilder.Entity<UnorderedPurchaseItemCart>().ToTable("UnorderedPurchaseItemCart");
        }
    }
}