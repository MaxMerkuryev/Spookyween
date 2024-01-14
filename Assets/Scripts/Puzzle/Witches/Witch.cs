using DG.Tweening;
using PickupableSystem;
using SfxSystem;
using UnityEngine;
using Vampires;

namespace Witches {
	public class Witch : PickupableHolder {
		[SerializeField] private GameObject _fire;
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private VampireEyeball[] _eyes;

		public override string ActionName => "witch";

		private WitchsPuzzle _witchsPuzzle;

		public void Init(WitchsPuzzle witchsPuzzle) {
			_witchsPuzzle = witchsPuzzle;
		}

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			if (!_witchsPuzzle.IsActive) {
				SfxPlayer.Play(SfxType.WitchReject);
				return;
			}
			
			base.Pickup(pickupable, customPath, useCustomOrientation);
			Enabled = false;
			SfxPlayer.Play(SfxType.WitchDie);
			DOTween.Sequence().InsertCallback(0.3f, () => {
				_fire.SetActive(true);
				_particles.Stop();
				foreach (VampireEyeball vampireEyeball in _eyes) {
					vampireEyeball.SetDead();
				}
				CurrentPickupable.gameObject.SetActive(false);
				_witchsPuzzle.OnWitchFire();
			});
		}
	}
}