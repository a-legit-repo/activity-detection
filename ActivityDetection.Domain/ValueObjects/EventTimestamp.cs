using System;

namespace ActivityDetection.Domain.ValueObjects
{
    public class EventTimestamp
    {
        public DateTime Value { get; }
        public EventTimestamp(DateTime value) { Value = value; }
    }
}