namespace InventoryService
{
    using System;

    internal class EventGridEvent<T>
    {
        public T Data { get; set; }
        public DateTime EventTime { get; set; }
        public string EventType { get; set; }
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
    }
}