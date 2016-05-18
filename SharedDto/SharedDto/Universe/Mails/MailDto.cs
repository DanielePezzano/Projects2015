using System;
using System.Runtime.Serialization;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Mails
{
    [DataContract]
    public class MailDto:IDto
    {
        
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public int SenderId { get; set; }
        [DataMember]
        public int ReceiverId { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}