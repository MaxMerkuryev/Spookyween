using System;
using System.Collections.Generic;

namespace Spookyween.Root {
	public class EventBus : IEventBus {
		public static IEventBus Instance { get; private set; }

		private readonly Dictionary<Type, List<IEventHandler>> _handlers;

		public EventBus() {
			if(Instance != null) {
				throw new Exception("Trying to create multiple Event Buses!!!");
			}

			_handlers = new Dictionary<Type, List<IEventHandler>>();
			Instance = this;
		}

		public void Dispose() {
			_handlers.Clear();
			Instance = null;
		}

		public void Register<TEvent>(IEventHandler handler) where TEvent : struct, IEvent {
			Type key = typeof(TEvent);

			if(!_handlers.ContainsKey(key)) {
				_handlers.Add(key, new List<IEventHandler>());
			}

			_handlers[key].Add(handler);
		}

		public void Unregister<TEvent>(IEventHandler handler) where TEvent : struct, IEvent {
			Type key = typeof(TEvent);

			if (_handlers.ContainsKey(key)) {
				_handlers[key].Remove(handler);
			}
		}

		public void Invoke<TEvent>(TEvent @event) where TEvent : struct, IEvent {
			Type key = typeof(TEvent);
			if (!_handlers.ContainsKey(key)) return;

			foreach (IEventHandler handler in _handlers[key]) {
				(handler as IEventHandler<TEvent>).Handle(@event); 
			}
		}
	}

	public interface IEventBus {
		void Register<TEvent>(IEventHandler handler) where TEvent : struct, IEvent;
		void Unregister<TEvent>(IEventHandler handler) where TEvent : struct, IEvent;
		void Invoke<TEvent>(TEvent @event) where TEvent : struct, IEvent;
	}

	public interface IEvent { }

	public interface IEventHandler { }

	public interface IEventHandler<TEvent> : IEventHandler where TEvent : struct, IEvent {
		void Handle(TEvent @event);
	}
}
