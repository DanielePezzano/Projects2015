using System.Runtime.Serialization;

namespace SharedDto.BaseClasses
{
    [DataContract]
    public class BaseDto
    {
         [DataMember]
         public int Id { get; set; }
    }
}