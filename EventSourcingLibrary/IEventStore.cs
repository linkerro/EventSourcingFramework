using System;
using System.Collections.Generic;

namespace EventSourcingLibrary
{
    public interface IEventStore
    {
        IEvent Store(IEvent eventToStore);
        IList<IEvent> ReadByIds(IEnumerable<Guid> eventIds);
        void Index(IEvent eventToIndex, IndexInfo indexInfo);
        void Index(IList<IEvent> eventsToIndex, IndexInfo indexInfo);
    }
}
