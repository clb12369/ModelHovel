using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class StashItem : HasId
{
    [Key]
    [Required]
    public int ItemID { get; set; }
    public string ItemType { get; set; }
    public string Manufacturer { get; set; }
    public string ItemNumber { get; set; }
    public string Scale { get; set; }
    public int ItemName {get;set;}
    public string Comments { get; set; }
    public int ListId { get; set; }
    public StashList List { get; set; }

    // public int UserID { get; set; }
    // public ApplicationUser User { get; set; }

}

public class StashList : HasId {
    [Key]
    [Required]
    public int ItemID { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public List<StashItem> StashItems { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = new ApplicationUser();
}

// public class Board : HasId {
//     [Required]
//     public int Id { get; set; }
//     [Required]
//     public string Title { get; set; }
//     [Required]
//     public List<CardList> Lists { get; set; }
// }

// declare the DbSet<T>'s of our DB context, thus creating the tables
public partial class DB : IdentityDbContext<ApplicationUser> {
    public DbSet<ApplicationUser> Members { get; set; }
    public DbSet<StashList> Lists { get; set; }
    public DbSet<StashItem> Items { get; set; }
}

// create a Repo<T> services
public partial class Handler {
    public void RegisterRepos(IServiceCollection services){
        Repo<StashItem>.Register(services, "Items");
        Repo<StashList>.Register(services, "Lists");
        Repo<ApplicationUser>.Register(services, "Members",
            d => d.Include(b => b.StashLists).ThenInclude(l => l.StashItems));
    }
}