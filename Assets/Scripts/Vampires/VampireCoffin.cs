using DG.Tweening;
using PickupableSystem;
using UnityEngine;

namespace Vampires {
	public class VampireCoffin : PickupableHolder {
		[SerializeField] private Transform _cap;
		[SerializeField] private Vampire _sirDracula;

		private bool _opened;
		public override bool Enabled => _opened && CurrentPickupable == null;

		public void Open() {
			_opened = true;
			_cap.DORotate(new Vector3(0f, 0f, 180f), 1f).SetEase(Ease.OutCirc);
		}

		public void Close() {
			_opened = false;
			_cap.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.InCirc);
		}

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			base.Pickup(pickupable, customPath, useCustomOrientation);
			DOTween.Sequence().InsertCallback(1f, () => {
				_sirDracula.Die();
			});
		}
	}
}