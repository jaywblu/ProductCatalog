using Infrastructure.Entities.CustomerData;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CustomerDataContext(DbContextOptions<CustomerDataContext> options) : DbContext(options)
{
    public virtual DbSet<CustomerTypeEntity> CustomerTypes { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<CustomerProfileEntity> CustomersProfiles { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<CampaignEntity> Campaigns { get; set; }
    public virtual DbSet<ActiveCampaignEntity>  ActiveCampaigns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerTypeEntity>()
            .HasIndex(x => x.Name)
            .IsUnique();

        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<ActiveCampaignEntity>()
            .HasKey(x => new { x.CampaignId, x.CustomerTypeId });

        modelBuilder.Entity<ActiveCampaignEntity>()
            .HasOne(o => o.Campaign)
            .WithMany(m => m.ActiveCampaigns)
            .HasForeignKey(f => f.CampaignId);

        modelBuilder.Entity<ActiveCampaignEntity>()
            .HasOne(o => o.CustomerType)
            .WithMany(m => m.ActiveCampaigns)
            .HasForeignKey(f => f.CustomerTypeId);
    }
}
