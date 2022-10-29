using System;
using DG.Tweening;
using SfxSystem;
using UnityEngine;

namespace Ui {
	public class Tutor : MonoBehaviour {
		[SerializeField] private RectTransform _wasd;
		[SerializeField] private RectTransform _space;
		
		private void Awake() {
			_wasd.localScale = _space.localScale = Vector3.zero;
			DOTween.Sequence()
				.InsertCallback(1f, () => ShowTutor(_wasd))
				.InsertCallback(2f, () => ShowTutor(_space))
				.InsertCallback(5f, () => HideTutor(_wasd))
				.InsertCallback(6f, () => HideTutor(_space))
				.InsertCallback(10f, () => gameObject.SetActive(false));
		}

		private void ShowTutor(RectTransform tutor) {
			tutor.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
			SfxPlayer.Play(SfxType.Pickup);
		}

		private void HideTutor(RectTransform tutor) {
			tutor.DOScale(0f, 0.5f).SetEase(Ease.InBack);
			SfxPlayer.Play(SfxType.Pickup);
		}
	}
}