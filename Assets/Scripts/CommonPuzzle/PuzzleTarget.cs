using System;
using DG.Tweening;
using UnityEngine;

namespace CommonPuzzle {
	public class PuzzleTarget : MonoBehaviour {
		[SerializeField] private Rigidbody _target;
		[SerializeField] private GameObject _targetBlocker;
		[SerializeField] private ParticleSystem _prepare;
		[SerializeField] private GameObject _explosion;

		public void Activate(Action onComplete) {
			_prepare.Play();
			DOTween.Sequence()
				.Insert(0f, _targetBlocker.transform.DOScale(1.5f, 2f).SetEase(Ease.OutBounce))
				.InsertCallback(3f, () => {
					_targetBlocker.SetActive(false);
					_explosion.SetActive(true);
					_target.isKinematic = false;
					onComplete?.Invoke();
				});
		}
	}
}