using System.Collections.Generic;

public interface IApplicationUserRepository {
    IEnumerable<ApplicationUser> Members { get; }
}