﻿using System;

namespace Accounts.Domain.SeedWork.Messages
{
    public abstract class Message
    {
        protected Message(
            string correlationIToken)
        {
            DateCreated = DateTime
                .UtcNow;

            MessageType = GetType()
                .Name;

            CorrelationToken = correlationIToken;
        }

        public DateTime DateCreated { get; }
        public string MessageType { get; }
        public string CorrelationToken { get; }
    }
}
