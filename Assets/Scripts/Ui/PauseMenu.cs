using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class PauseMenu : UiMenu {
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _quitButton;
		
		public static event Action OnShow;
		public static event Action OnHide;
		
		public override void Init(UiCanvas uiCanvas) {
			base.Init(uiCanvas);
			_playButton.onClick.AddListener(() => _uiCanvas.SetState(UiCanvas.State.Play));

			// todo main menu 
			_quitButton.onClick.AddListener(() => {});
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
		}
	}
}