using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingLibrary
{
    public class Dispatcher
    {
        private Dictionary<Type, List<Transform>> _pipelines = new Dictionary<Type, List<Transform>>();

        public IList<Type> DispatchedEventTypes
        {
            get
            {
                return _pipelines.Keys.ToList();
            }
        }

        public delegate IEvent Transform(IEvent receivedEvent);

        public void RegisterPipeLine(IList<Type> eventTypes, IList<Transform> actions)
        {
            eventTypes
                .ToList()
                .ForEach(t => _pipelines.Add(t, actions.ToList()));
        }

        public void Dispatch(IEvent eventToDispatch)
        {
            var type = eventToDispatch.GetType();
            if (_pipelines.ContainsKey(type))
            {
                var pipeline = _pipelines[type];
                for (int index = 0; index < pipeline.Count; index++)
                {
                    pipeline[index](eventToDispatch);
                }
            }
        }

    }
}
