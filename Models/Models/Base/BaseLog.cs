using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Base
{
    [DataContract]
    public class BaseLogEntity : BaseEntity
    {
        [DataMember]
        public string MethodCaller { get; set; }
        [DataMember]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }
        [DataMember]
        public string ParametersValue { get; set; }
    }
}
