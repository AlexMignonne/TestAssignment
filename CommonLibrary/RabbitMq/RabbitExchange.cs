﻿using System;
using System.Collections.ObjectModel;

namespace CommonLibrary.RabbitMq
{
    public abstract class RabbitExchange
    {
        protected RabbitExchange(
            IRabbitProto proto,
            string name,
            RabbitExchangeType type,
            bool durable,
            bool autoDelete,
            ReadOnlyDictionary<string, object>? arguments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(
                    $"{nameof(Name)} cannot be empty.");

            Proto = proto;
            Name = name;
            Type = type;
            Durable = durable;
            AutoDelete = autoDelete;
            Arguments = arguments;
        }

        public IRabbitProto Proto { get; }
        public string Name { get; }
        public RabbitExchangeType Type { get; }
        public bool Durable { get; }
        public bool AutoDelete { get; }
        public ReadOnlyDictionary<string, object>? Arguments { get; }
    }
}
