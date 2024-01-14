using UnityEngine;

namespace Spookyween.Root {
	public class Bootstrap : MonoBehaviour {
		private ServiceLocator _serviceLocator;
		private EventBus _eventBus;

		private void Awake() {
			_serviceLocator = new ServiceLocator();
			_eventBus = new EventBus();
			DontDestroyOnLoad(gameObject);	
		}

		private void OnApplicationQuit() {
			_serviceLocator.Dispose();
			_eventBus.Dispose();
		}
	}
}
