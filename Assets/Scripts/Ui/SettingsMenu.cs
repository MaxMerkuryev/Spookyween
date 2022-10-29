using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class SettingsMenu : MonoBehaviour{
		[SerializeField] private Button _quitButton;
		[SerializeField] private SettingUiPanel[] _settings;
		
		public void Init(Action hideAction) {
			_quitButton.onClick.AddListener(() => hideAction?.Invoke());
			foreach(SettingUiPanel setting in _settings) setting.Init();
		}
	}
}