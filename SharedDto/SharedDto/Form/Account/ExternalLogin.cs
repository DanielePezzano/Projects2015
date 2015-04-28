
using System.Runtime.Serialization;
namespace SharedDto.Form.Account
{
    [DataContract]
    public class ExternalLogin
    {
        [DataMember]
        public string Provider { get; set; }
        [DataMember]
        public string ProviderDisplayName { get; set; }
        [DataMember]
        public string ProviderUserId { get; set; }
    }
}
