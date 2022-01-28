using Microsoft.EntityFrameworkCore;

public class CSDBContext : DbContext
{
    //Empty
    public CSDBContext() : base(){}

    //Need to pass options, call parent that takes options
    public CSDBContext(DbContextOptions options) : base(options) {}

    public DbSet<Store> Stores { get; set; }

    //I stop folling here because do I use Microsoft.EntityFrameworkCore.SqlServer or
    //Npgsql.EntityFrameworkCore.PostgreSQL?
    //dotnet ef migrations add initMig -c CSDBContext.cs --startup-project ../WebAPI

    
    //if you want to manually modify certain columns, properties...
    //map them here
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
        .Property(restaurant => restaurant.Id)
        .ValueGeneratedOnAdd();
        modelBuilder.Entity<Review>()
        .Property(review => review.Id)
        .ValueGeneratedOnAdd();
    }*/
}