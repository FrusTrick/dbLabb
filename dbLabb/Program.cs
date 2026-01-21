using dbLabb.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using dbLabb.Models;

namespace dbLabb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<InterestingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/getallpersons", async (InterestingDbContext context) =>
            {
                try
                {
                    var fetchedUsers = await context.Persons.ToListAsync();
                    if (fetchedUsers == null || fetchedUsers.Count == 0)
                    {
                        return Results.NoContent();
                    }
                    else
                    {
                        return Results.Ok(fetchedUsers);
                    }
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapGet("/getspecific/Interests{id}", async (InterestingDbContext context, int id) =>
            {
                try
                {
                    var fetchedIntrests = await context.Interests
                    .Where(i => i.PersonId == id)
                    .ToListAsync();
                    if (fetchedIntrests == null || fetchedIntrests.Count == 0)
                    {
                        return Results.NoContent();
                    }
                    else
                    {
                        return Results.Ok(fetchedIntrests);
                    }
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapGet("/getspecific/Links{id}", async (InterestingDbContext context, int id) =>
            {
                try
                {
                    var fetchedLinks = await context.Links
                    .Where(i => i.Interest.PersonId == id)
                    .ToListAsync();

                    if (fetchedLinks == null || fetchedLinks.Count == 0)
                    {
                        return Results.NoContent();
                    }
                    else
                    {
                        return Results.Ok(fetchedLinks);
                    }
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/addtoperson/interests", async (InterestingDbContext context, Interest newInterest) =>
            {
                try
                {
                    var foundPerson = await context.Persons
                    .FindAsync(newInterest.PersonId);

                    var transaction = await context.Database.BeginTransactionAsync();


                    if (foundPerson == null)
                    {
                        transaction.Rollback();
                        return Results.NotFound();
                    }
                    else
                    {
                        var interest = new Interest
                        {
                            PersonId = newInterest.PersonId,
                            Name = newInterest.Name
                        };
                        await context.Interests.AddAsync(interest);

                        await transaction.CommitAsync();
                        await context.SaveChangesAsync();

                        return Results.Ok(newInterest);
                    }
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/addlink/person={personId}&interest={interestId}", async (InterestingDbContext context, Link link, int personId, int interestId) =>
            {
                try
                {
                    var transaction = await context.Database.BeginTransactionAsync();
                    var fetchedPerson = await context.Persons
                        .Where(p => p.Id == personId)
                        .Include(p => p.Interests)
                        .ToListAsync();

                    if (fetchedPerson != null)
                    {
                        var foundInterest = new Interest();

                        foreach (var person in fetchedPerson)
                        {
                            var interestList = person.Interests;
                            foreach (var i in interestList)
                            {
                                if (i.Id == interestId)
                                {
                                    foundInterest = i;
                                }
                            }
                        }

                        var newLink = new Link
                        {
                            Url = link.Url,
                            IntrestId = foundInterest.Id
                        };
                        await context.Links.AddAsync(newLink);
                        await transaction.CommitAsync();
                        await context.SaveChangesAsync();

                        return Results.Ok(newLink);
                    }
                    else
                    {
                        transaction.Rollback();
                        return Results.NotFound();
                    }

                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.Run();
        }
    }
}
