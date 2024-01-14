using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spookyween.Root {
	public class ServiceLocator : IServiceLocator {
		public static IServiceLocator Instance { get; private set; }

		private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

		public ServiceLocator() {
			if (Instance != null) {
				Debug.LogError("There can be only one :)");
				return;
			}

			Instance = this;
		}

		public void Dispose() {
			Instance = null;
			foreach (var item in _services) {
				item.Value.Dispose();
			}
		}

		public T Get<T>() where T : IService {
			Type type = typeof(T);
			if (_services.TryGetValue(type, out IService service))
				return (T)service;
			Debug.LogError($"Service of type {type} is not registered! Return default!");
			return default;
		}

		public void Register(Type type, IService service) {
			if (!_services.TryAdd(type, service)) {
				Debug.LogError($"Type {type} is already registered!");
			}
		}

		public void Unregister(Type type) {
			if (_services.ContainsKey(type))
				_services.Remove(type);
			else
				Debug.LogError($"Service of type {type} is not registered, nothing to unregister");
		}
	}

	public interface IServiceLocator {
		void Register(Type type, IService service);
		void Unregister(Type type);
		T Get<T>() where T : IService;
	}

	public interface IService {
		void Dispose();
	}
}