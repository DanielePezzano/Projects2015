using System;

namespace SharedDto.Interfaces
{
    public interface IQueue
    {
        string Status { get; set; }
        DateTime FinishDateTime { get; set; }
    }
}