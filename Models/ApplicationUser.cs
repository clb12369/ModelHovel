using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationUser : IdentityUser, HasId
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ModelingInterest { get; set; }
    public int ItemID { get; set; }
    public List<StashList> StashLists { get; set; } = new List<StashList>();
}