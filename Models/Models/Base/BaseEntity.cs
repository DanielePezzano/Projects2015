using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Base
{
    [DataContract]
    public class BaseEntity
    {
        [Required()]
        [DataMember]
        public int Id { get; set; }
        [Required()]
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [Required()]
        [DataMember]
        public DateTime UpdatedAt { get; set; }
    }
}
