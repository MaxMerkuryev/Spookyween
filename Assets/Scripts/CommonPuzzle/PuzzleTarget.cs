using System;
using Alchemy;
using DG.Tweening;
using SfxSystem;
using UnityEngine;

namespace CommonPuzzle {
	public class PuzzleTarget : MonoBehaviour {
		[SerializeField] private Rigidbody _target;
		[SerializeField] private GameObject _targetBlocker;
		[SerializeField] private ParticleSystem _prepare;
		[SerializeField] private ParticleSystem _explosion;

		public void Activate(Action onComplete = null) {
			_prepare.Play();
			DOTween.Sequence()
				.Insert(0f, _targetBlocker.transform.DOScale(1.5f, 2f).SetEase(Ease.OutBounce))
				.InsertCallback(0f, () => SfxPlayer.Play(SfxType.PuzzleTargetActivate))
				.InsertCallback(3f, () => {
					_targetBlocker.SetActive(false);
					_explosion.Play();
					_target.isKinematic = false;
					onComplete?.Invoke();
					PotionEffectController.INSTANCE.End();
				});
		}
	}
}