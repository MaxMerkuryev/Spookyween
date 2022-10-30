using System;
using DG.Tweening;
using SfxSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui {
	public class FinalUi : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private TextMeshProUGUI _subTitle;
		[SerializeField] private TextMeshProUGUI _anotherSubTitle;

		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _quitButton;

		private void Awake() {
			_restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
			_quitButton.onClick.AddListener(Application.Quit);
		}

		private void OnEnable() {
			SfxPlayer.Play(SfxType.Final);
			DOTween.Sequence()
				.Insert(3f, _subTitle.DOFade(1f, 2f))
				.Insert(5f, _anotherSubTitle.DOFade(1f, 2f))
				.InsertCallback(7f, () => {
					SfxPlayer.Play(SfxType.Pickup);
					_restartButton.gameObject.SetActive(true);
					_quitButton.gameObject.SetActive(true);
				});
		}
	}
}