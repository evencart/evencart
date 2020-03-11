#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;

namespace EvenCart.Core.Services.Events
{
    public class EventPublisherService : IEventPublisherService
    {
        public void Publish<T>(T entity, EventType eventType) where T : FoundationEntity
        {
            switch (eventType)
            {
                case EventType.Insert:

                    var iConsumers = DependencyResolver.ResolveMany<IFoundationEntityInserted<T>>();
                    //first find out all the consumers
                    foreach (var ec in iConsumers)
                    {
                        ec.OnInserted(entity);
                    }
                    break;
                case EventType.Update:
                    var uConsumers = DependencyResolver.ResolveMany<IFoundationEntityUpdated<T>>();
                    //first find out all the consumers
                    foreach (var ec in uConsumers)
                    {
                        ec.OnUpdated(entity);
                    }
                    break;
                case EventType.Delete:
                    var dConsumers = DependencyResolver.ResolveMany<IFoundationEntityDeleted<T>>();
                    //first find out all the consumers
                    foreach (var ec in dConsumers)
                    {
                        ec.OnDeleted(entity);
                    }
                    break;
            }
        }

        public T Filter<T>(T input)
        {
            var dFilters = DependencyResolver.ResolveMany<IFoundationFilter<T>>();
            foreach (var ec in dFilters)
            {
                input = ec.Filter(input);
            }
            return input;
        }

        public void Publish(string eventName, object[] data = null)
        {
            var consumers = DependencyResolver.ResolveMany<IEventCapture>()
                .Where(x => x.EventNames != null &&
                            x.EventNames.Contains(eventName, StringComparer.InvariantCultureIgnoreCase));

            foreach (var c in consumers)
            {
                c.Capture(eventName, data);
            }
        }
    }
}