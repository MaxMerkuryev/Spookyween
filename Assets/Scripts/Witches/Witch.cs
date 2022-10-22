using System;
using DG.Tweening;
using PickupableSystem;
using UnityEngine;
using Vampires;

namespace Witches {
	public class Witch : PickupableHolder {
		[SerializeField] private GameObject _fire;
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private VampireEyeball[] _eyes;

		private void Awake() {
			Enabled = false;
		}

		public void Activate() {
			if(CurrentPickupable) return;
			Enabled = true;
		}

		public void Deactivate() {
			Enabled = false;
		}

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			base.Pickup(pickupable, customPath, useCustomOrientation);
			Enabled = false;
			DOTween.Sequence().InsertCallback(0.5f, () => {
				_fire.SetActive(true);
				_particles.Stop();
				foreach (VampireEyeball vampireEyeball in _eyes) {
					vampireEyeball.SetDead();
				}
				CurrentPickupable.gameObject.SetActive(false);
			});
		}
	}
}