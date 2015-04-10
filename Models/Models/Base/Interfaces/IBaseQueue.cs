using Models.Base.Enum;
using Models.Queues.Enum;
using System;

namespace Models.Base.Interfaces
{
    public interface IBaseQueue
    {        
        DateTime FinishAt { get; set; }
        QueueStatus Status { get; set; }
    }
}
