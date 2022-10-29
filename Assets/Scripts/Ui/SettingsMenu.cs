using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class SettingsMenu : MonoBehaviour{
		[SerializeField] private Button _quitButton;
		
		public void Init(Action hideAction) {
			_quitButton.onClick.AddListener(() => hideAction?.Invoke());
		}
	}
}