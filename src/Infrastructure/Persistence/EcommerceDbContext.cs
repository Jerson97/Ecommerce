using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence;

public class EcommerceDbContext : IdentityDbContext<User>{

    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options){

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Product>()
        .HasMany(p => p.Reviews)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Product>()
        .HasMany(p => p.Images)
        .WithOne(i => i.Product)
        .HasForeignKey(i => i.ProductId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ShoppingCart>()
        .HasMany(p => p.ShoppingCartItems)
        .WithOne(i => i.ShoppingCart)
        .HasForeignKey(i => i.ShoppingCartId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);



        builder.Entity<User>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<User>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
    }

    public DbSet<Product>? products {get;set;}
    public DbSet<Category>? Categories {get;set;}
    public DbSet<Image>? Images {get;set;}
    public DbSet<Address>? Addresses {get;set;}
    public DbSet<Order>? Orders {get;set;}
    public DbSet<OrderItem>? OrderItems {get;set;}
    public DbSet<Review>? reviews {get;set;}
    public DbSet<ShoppingCart>? ShoppingCarts {get;set;}
    public DbSet<ShoppingCartItem>? ShoppingCartItems {get;set;}
    public DbSet<Country>? Countries {get;set;}
    public DbSet<OrderAddres>? OrderAddres {get;set;}
}