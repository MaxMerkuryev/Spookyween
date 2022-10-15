using UnityEngine;

namespace Ui {
	// thing that should be locked on pause
	public abstract class Lockable : MonoBehaviour{
		private bool _locked;
		
		private void OnEnable() {
			PauseMenu.OnShow += Lock;
			PauseMenu.OnHide += Unlock;
		}

		private void OnDisable() {
			PauseMenu.OnShow -= Lock;
			PauseMenu.OnHide -= Unlock;
		}

		private void Lock() {
			_locked = true;
		}

		private void Unlock() {
			_locked = false;
		}

		private void Update() {
			if (_locked) return;
			OnUpdate();
		}

		private void FixedUpdate() {
			if (_locked) return;
			OnFixedUpdate();
		}

		private void LateUpdate() {
			if (_locked) return;
			OnLateUpdate();
		}
		
		protected virtual void OnUpdate(){}
		protected virtual void OnFixedUpdate(){}
		protected virtual void OnLateUpdate(){}
	}
}