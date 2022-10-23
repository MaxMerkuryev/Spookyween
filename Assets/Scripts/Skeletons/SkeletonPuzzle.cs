using Alchemy;
using DG.Tweening;
using Pumpkins;
using UnityEngine;

namespace Skeletons {
	public class SkeletonPuzzle : PotionEffectReceiver {
		[SerializeField] private Pumpkin _pumpkin;
		[SerializeField] private GameObject _targetBlocker;
		[SerializeField] private ParticleSystem _prepare;
		[SerializeField] private GameObject _explosion;
		[SerializeField] private Skeleton[] _skeletons;

		private int _count;
		
		private Rigidbody _pumpkinRigidbody;
		public bool Active { get; private set; }
		
		protected override PotionType _potionType => PotionType.Hypnosis;
		
		protected override void OnPotionDrinkAction() {
			Active = true;
		}

		protected override void OnPotionEndAction() {
			Active = false;
			foreach (Skeleton skeleton in _skeletons) {
				skeleton.DeHypnotize();
			}

			_count = 0;
		}

		public void SkeletonHypnotized() {
			_count++;

			if (_count < _skeletons.Length) return;

			_prepare.Play();
			DOTween.Sequence()
				.Insert(0f, _targetBlocker.transform.DOScale(1.5f, 2f).SetEase(Ease.OutBounce))
				.InsertCallback(3f, () => {
					_targetBlocker.SetActive(false);
					_explosion.SetActive(true);
					_pumpkin.gameObject.SetActive(true);
					_pumpkinRigidbody.isKinematic = false;

					foreach (Skeleton skeleton in _skeletons) {
						skeleton.Die();
					}
				});
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				_count = 5;
				SkeletonHypnotized();
			}
		}

		// yeah, another duct tape
		private void Awake() {
			_pumpkinRigidbody = _pumpkin.GetComponent<Rigidbody>();
			_pumpkinRigidbody.isKinematic = true;
			foreach(Skeleton skeleton in _skeletons) skeleton.Init(this);
		}
	}
}