using System;

namespace EventSourcingLibrary
{
    public  interface IEvent
    {
        Guid Id { get; }
        DateTime Timestamp { get;  }
        string EventType{get;}
        string Blob { get;  }
        IEvent Parse(string blob);
    }
}
