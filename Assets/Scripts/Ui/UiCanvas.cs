using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui {
	public class UiCanvas : MonoBehaviour {
		[SerializeField] private UiMenu _hud;
		[SerializeField] private UiMenu _pause;

		public enum State {
			Play,
			Pause,
			Menu
		}

		private Dictionary<State, UiMenu> _menus;
		private State _currentState;
		
		public void SetState(State state) {
			_menus[_currentState].Hide();
			_currentState = state;
			_menus[_currentState].Show();
		}
		
		private void Awake() {
			_menus = new Dictionary<State, UiMenu>() { 
				{ State.Play, _hud }, 
				{ State.Pause, _pause} 
			};

			foreach (KeyValuePair<State, UiMenu> menu in _menus) {
				menu.Value.Init(this);
			}
			
			SetState(State.Play);
			LockCursor();
		}
		
		private void OnEnable() {
			PauseMenu.OnShow += UnlockCursor;
			PauseMenu.OnHide += LockCursor;
		}

		private void OnDisable() {
			PauseMenu.OnShow -= UnlockCursor;
			PauseMenu.OnHide -= LockCursor;
		}
		
		private void LockCursor() {
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void UnlockCursor() {
			Cursor.lockState = CursorLockMode.None;
		}
		
		// i hate switch
		private void Update() {
			bool pauseKeyDown = Input.GetKeyDown(KeyCode.Escape);

#if UNITY_EDITOR
			pauseKeyDown = Input.GetKeyDown(KeyCode.BackQuote); // because... reasons
#endif

			if (pauseKeyDown) {
				switch (_currentState) {
					case State.Play:
						SetState(State.Pause);
						break;
					case State.Pause:
						SetState(State.Play);
						break;
					case State.Menu:
						break;
					default:
						break;
				}
			}
		}
	}
}