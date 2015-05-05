using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Logs;

namespace Models.Base
{
    [DataContract(IsReference = true)]
    [KnownType(typeof (GalaxyLog))]
    [KnownType(typeof (UserLog))]
    public class BaseLogEntity : BaseEntity
    {
        [DataMember]
        public string MethodCaller { get; set; }

        [DataMember]
        [Display(Name = "Description", ResourceType = typeof (Resources))]
        public string Description { get; set; }

        [DataMember]
        public string ParametersValue { get; set; }
    }
}