using System;
using TMPro;
using UnityEngine;

namespace Ui {
	public class InteractionUi : MonoBehaviour {
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private GameObject _actionKeyContainer;
		[SerializeField] private TextMeshProUGUI _actionKey;
		[SerializeField] private TextMeshProUGUI _actionName;

		private const float _clearTime = 0.1f;
		private float _currentClearTime;
		//private bool _clear;
		
		private static Action<string, string> _updateUi;

		public static void Invoke(string actionKey, string actionName) {
			_updateUi?.Invoke(actionKey, actionName);
		}

		private void Awake() => Clear();
		private void OnEnable() => _updateUi += UpdateUi;
		private void OnDisable() => _updateUi -= UpdateUi;

		private void UpdateUi(string actionKey, string actionName) {
			_canvasGroup.alpha = 1f;
			_actionKeyContainer.SetActive(!string.IsNullOrEmpty(actionKey));
			_actionKey.text = actionKey;
			_actionName.text = actionName;
			_currentClearTime = Time.time + _clearTime;
			//_clear = false;
		}

		private void Clear() {
			_canvasGroup.alpha = 0f;
			//_clear = true;
		}
		
		private void Update() {
			//if (_clear) return;
			if (Time.time < _currentClearTime) return;
			Clear();
		}
	}
}