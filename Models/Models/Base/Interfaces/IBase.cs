using System;

namespace Models.Base.Interfaces
{
    public interface IBase
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
