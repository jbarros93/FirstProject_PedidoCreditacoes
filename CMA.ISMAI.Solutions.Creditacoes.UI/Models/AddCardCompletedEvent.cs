﻿using System;

namespace CMA.ISMAI.Solutions.Creditacoes.UI.Models
{
    public class AddCardCompletedEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DueTime { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessageType { get;  set; }
        public Guid AggregateId { get; set; }
    }
}
