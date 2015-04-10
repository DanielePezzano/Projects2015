using Models.Base.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class BaseEntity : IBase
    {
        [Required()]
        public int Id { get; set; }
        [Required()]        
        public DateTime CreatedAt { get; set; }
        [Required()]
        public DateTime UpdatedAt { get; set; }
    }
}
