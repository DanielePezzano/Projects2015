using System.ComponentModel.DataAnnotations;
using Models.Base;
using System.Runtime.Serialization;
using Models.Tech.Enum;

namespace Models.Tech
{
    [DataContract(IsReference=true)]
    public class TechRequisiteNode : BaseEntity
    {
        [DataMember]
        public virtual Technology Technology { get; set; }
        [DataMember]
        [EnumDataType(typeof(RequisiteType))]
        public RequisiteType RequisiteType { get; set; }
    }
}