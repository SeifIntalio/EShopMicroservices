using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Discount.Grpc.Data;

public class DiscountContext:DbContext
{
    public DbSet<Coupon> Coupons { get; set; }= default!;
    public DiscountContext(DbContextOptions<DiscountContext>options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id=1,ProductName="IPhone X",Description="IPhone Discription", Amount=10},
            new Coupon { Id=2,ProductName="Samsung S24 Ultra",Description= "Samsung Discription", Amount=10}
            );
    }
}
