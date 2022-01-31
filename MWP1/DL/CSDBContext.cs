using Microsoft.EntityFrameworkCore;

public class CSDBContext : DbContext
{
    //Empty
    public CSDBContext() : base(){}

    //Need to pass options, call parent that takes options
    public CSDBContext(DbContextOptions options) : base(options) {}

    public DbSet<Store> Stores { get; set; }
    public DbSet<Game> Games { get; set; }

    //I stop folling here because do I use Microsoft.EntityFrameworkCore.SqlServer or
    //Npgsql.EntityFrameworkCore.PostgreSQL?


    //After every change to models
    //dotnet ef migrations add initMig2 -c CSDBContext --startup-project ../WebAPI
    //dotnet ef database update --startup-project ../WebAPI


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