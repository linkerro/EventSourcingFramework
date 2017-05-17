using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventSourcingLibrary;
using System.Collections.Generic;
using static EventSourcingLibrary.Dispatcher;
using System.Linq;

namespace EventSourcingFrameworkSpecs
{
    [TestClass]
    public class DispatcherBaseClassSpecs
    {

        [TestMethod]
        public void ExposesActionDelegate()
        {
            Transform test = e => e;
        }

        [TestMethod]
        public void ShouldRegisterPipeline()
        {
            var dispatcher = new Dispatcher();
            var eventTypes = new List<Type>();
            var actions = new List<Transform>();
            dispatcher.RegisterPipeLine(eventTypes, actions);
        }
        [TestMethod]
        public void ShouldExposeHandledEventTypes()
        {
            var dispatcher = new Dispatcher();
            var eventTypes = dispatcher.DispatchedEventTypes;
            Assert.IsNotNull(eventTypes);
        }

        [TestMethod]
        public void ShouldReturnTheEventTypesThatWereRegistered()
        {
            var dispatcher = new Dispatcher();
            var eventTypes = new List<Type> { typeof(IEvent) };
            var actions = new List<Transform>();
            dispatcher.RegisterPipeLine(eventTypes, actions);
            var dispatchedEventTypes = dispatcher.DispatchedEventTypes.ToList();
            CollectionAssert.AreEqual(eventTypes, dispatchedEventTypes);
        }

        [TestMethod]
        public void ShouldDispatchAnEvent()
        {
            var testEventDispatcher = new TestEventDispatcher();
            var testEvent = new DispatcherTestEvent();
            testEventDispatcher.Dispatch(testEvent);
            Assert.AreEqual(true, testEventDispatcher.TestTransformHasBeenCalled);
        }
    }

    class DispatcherTestEvent : IEvent
    {
        public DispatcherTestEvent()
        {
        }

        public Guid Id => Guid.Empty;

        public DateTime Timestamp => DateTime.MinValue;

        public string EventType => GetType().Name;

        public string Blob => string.Empty;

        public IEvent Parse(string blob)
        {
            throw new NotImplementedException();
        }
    }

    internal class TestEventDispatcher : Dispatcher
    {
        public bool TestTransformHasBeenCalled = false;
        public TestEventDispatcher()
        {
            RegisterPipeLine(new List<Type>
            {
                typeof(DispatcherTestEvent)
            },
            new List<Transform>
            {
                (Transform)(e => {
                    TestTransformHasBeenCalled = true;
                    return e;
                })
            });
        }
    }
}
