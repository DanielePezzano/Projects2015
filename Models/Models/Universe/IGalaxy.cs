using System;
using System.Collections.Generic;
namespace Models.Universe
{
    public interface IGalaxy
    {
        string Name { get; set; }
        ICollection<Star> Stars { get; set; }
        ICollection<Models.Users.User> Users { get; set; }
    }
}
