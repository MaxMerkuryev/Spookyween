using System;
using DG.Tweening;
using UnityEngine;

namespace Vampires {
	public class VampirePuzzleTarget : MonoBehaviour {
		[SerializeField] private Transform _leftDoor;
		[SerializeField] private Transform _rightDoor;
		[SerializeField] private BoxCollider _collider;
		
		public void Open() {
			_collider.enabled = false;
			_leftDoor.DOLocalRotate(new Vector3(357.279083f,241.842957f,355.27832f), 1f);
			_rightDoor.DOLocalRotate(new Vector3(359.985107f,271.730988f,2.12159014f), 1f);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.T)) {
				//Open();
			}
		}
	}
}