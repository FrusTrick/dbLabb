using Microsoft.EntityFrameworkCore;
using dbLabb.Models;
namespace dbLabb.Data
{
    public class InterestingDbContext : DbContext
    {
        public InterestingDbContext(DbContextOptions<InterestingDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Interests)
                .WithOne(i => i.person)
                .HasForeignKey(i => i.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Interest>()
                .HasMany(i => i.Links)
                .WithOne(l => l.Interest)
                .HasForeignKey(l => l.IntrestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.PersonId)
                    .IsRequired();
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(e => e.IntrestId)
                    .IsRequired();
            });

            modelBuilder.Entity<Person>().HasData(
                new Person { 
                    Id = 1, 
                    Name = "John", 
                    LastName = "Doe", 
                    PhoneNumber = "+46704523256" },
                new Person { 
                    Id = 2, 
                    Name = "Jane", 
                    LastName = "Smith", 
                    PhoneNumber = "+46736259883" },
                new Person { 
                    Id = 3, 
                    Name = "Alice", 
                    LastName = "Johnson", 
                    PhoneNumber = "+46789012345" },
                new Person { 
                    Id = 4, 
                    Name = "Bob", 
                    LastName = "Brown", 
                    PhoneNumber = "+46712345678" },
                new Person { 
                    Id = 5, 
                    Name = "Eve", 
                    LastName = "Davis", 
                    PhoneNumber = "+46798765432" },
                new Person { 
                    Id = 6, 
                    Name = "Charlie", 
                    LastName = "Wilson", 
                    PhoneNumber = "+46756473829" }
            );
            modelBuilder.Entity<Interest>().HasData(
                new Interest { 
                    Id = 1, 
                    Name = "Photography", 
                    PersonId = 1 
                },
                new Interest { 
                    Id = 2, 
                    Name = "Hiking", 
                    PersonId = 1 
                },
                new Interest { 
                    Id = 3, 
                    Name = "Cooking", 
                    PersonId = 2 
                },
                new Interest { 
                    Id = 4, 
                    Name = "Reading", 
                    PersonId = 3 
                },
                new Interest { 
                    Id = 5, 
                    Name = "Traveling", 
                    PersonId = 4 
                },
                new Interest { 
                    Id = 6, 
                    Name = "Gaming", 
                    PersonId = 5 
                },
                new Interest {
                    Id = 7,
                    Name = "Gardening",
                    PersonId = 6 
                },
                new Interest {
                    Id = 8,
                    Name = "Cycling",
                    PersonId = 2 
                },
                new Interest {
                    Id = 9,
                    Name = "Music",
                    PersonId = 3 
                },
                new Interest {
                    Id = 10,
                    Name = "Yoga",
                    PersonId = 4 
                }
            );

            modelBuilder.Entity<Link>().HasData(
                new Link { 
                    Id = 1, 
                    Url = "https://www.photography.com", 
                    IntrestId = 1 
                },
                new Link { 
                    Id = 2, 
                    Url = "https://www.hikingadventures.com", 
                    IntrestId = 2 
                },
                new Link { 
                    Id = 3, 
                    Url = "https://www.cookingrecipes.com", 
                    IntrestId = 3 
                },
                new Link { 
                    Id = 4, 
                    Url = "https://www.bookclub.com", 
                    IntrestId = 4 
                },
                new Link { 
                    Id = 5, 
                    Url = "https://www.travelblog.com", 
                    IntrestId = 5 
                },
                new Link { 
                    Id = 6, 
                    Url = "https://www.gamingworld.com", 
                    IntrestId = 6 
                },
                new Link {
                    Id = 7,
                    Url = "https://www.gardeningtips.com",
                    IntrestId = 7 
                },
                new Link {
                    Id = 8,
                    Url = "https://www.cyclingroutes.com",
                    IntrestId = 8 
                },
                new Link {
                    Id = 9,
                    Url = "https://www.musiclovers.com",
                    IntrestId = 9 
                },
                new Link {
                    Id = 10,
                    Url = "https://www.yogapractice.com",
                    IntrestId = 10 
                }
            );

        }
    }
}
