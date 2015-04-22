using Models.Base;
using System.Runtime.Serialization;

namespace Models.Tech
{
    [DataContract]
    public class TechRequisiteNode : BaseEntity
    {
        [DataMember]
        public virtual Technology Technology { get; set; }
        [DataMember]
        public virtual Technology Requisite { get; set; }
    }
}