using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons
{
    public class PubSub
    {
        public interface IPayload {

        }

        public static readonly PubSub INSTANCE = new PubSub();

        private readonly Dictionary<Type, List<Action<object>>> subscriptions = new Dictionary<Type, List<Action<object>>>();

        public void Publish<TPayload>(TPayload message) where TPayload : IPayload {
            if (!subscriptions.TryGetValue(typeof(TPayload), out List<Action<object>> subscribers)) {
                return;
            }

            foreach (var subscriber in subscribers) {
                subscriber(message);
            }
        }

        public void Subscribe<TPayload>(Action<object> subscriber) where TPayload : IPayload {
            List<Action<object>> subscribers;
            if (!subscriptions.TryGetValue(typeof(TPayload), out subscribers)) {
                subscribers = new List<Action<object>>();
                subscriptions.Add(typeof(TPayload), subscribers);
            }

            subscribers.Add(subscriber);
        }
    }
}