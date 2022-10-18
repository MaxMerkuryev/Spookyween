using System;
using DG.Tweening;
using PickupableSystem;
using UnityEngine;

namespace Vampires {
	public class VampireCoffin : PickupableHolder {
		[SerializeField] private Transform _cap;
		[SerializeField] private Vampire _sirDracula;
		
		private bool _opened;
		public override bool Enabled => _opened && CurrentPickupable == null;

		private Action _onKill;
		
		public void Init(Action action) {
			_onKill = action;
		}
		
		public void Open() {
			if(_sirDracula.Dead) return;
			_opened = true;
			_cap.DOLocalRotate(new Vector3(0f, 0f, -180f), 1f).SetEase(Ease.OutCirc);
		}

		public void Close() {
			if(_sirDracula.Dead) return;
			_opened = false;
			_cap.DOLocalRotate(new Vector3(0f, 0f, 360f), 1f).SetEase(Ease.OutCirc);
		}

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			Vector3[] path = {
				Vector3.zero, 
				Vector3.forward * 5f,
				Vector3.forward
			};
			
			base.Pickup(pickupable, path, useCustomOrientation);
			DOTween.Sequence().InsertCallback(0.25f, () => {
				_sirDracula.Die();
				_onKill.Invoke();
			});
		}
	}
}