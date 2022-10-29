using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class PauseMenu : UiMenu {
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _settingsButton;
		[SerializeField] private Button _quitButton;

		[SerializeField] private GameObject _pausePanel;
		[SerializeField] private SettingsMenu _settingsPanel;
		
		public static event Action OnShow;
		public static event Action OnHide;
		
		public override void Init(UiCanvas uiCanvas) {
			base.Init(uiCanvas);
			_playButton.onClick.AddListener(() => _uiCanvas.SetState(UiCanvas.State.Play));
			_settingsButton.onClick.AddListener(ShowSettings);
			_quitButton.onClick.AddListener(Application.Quit);
			_settingsPanel.Init(HideSettings);
		}

		private void ShowSettings() {
			_pausePanel.SetActive(false);
			_settingsPanel.gameObject.SetActive(true);
		}

		public void HideSettings() {
			_pausePanel.SetActive(true);
			_settingsPanel.gameObject.SetActive(false);
		}
		
		public override void Show() {
			base.Show();
			Time.timeScale = 0f;
			OnShow?.Invoke();
		}

		public override void Hide() {
			base.Hide();
			Time.timeScale = 1f;
			OnHide?.Invoke();
			HideSettings();
		}
	}
}