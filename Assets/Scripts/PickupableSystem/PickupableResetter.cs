using System;
using UnityEngine;

namespace PickupableSystem {
	public class PickupableResetter : MonoBehaviour {
		private void OnTriggerExit(Collider other) {
			if (other.TryGetComponent(out Pickupable pickupable)) {
				pickupable.ResetPickupable();
			}
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = new Color(0, 1, 0, 0.1f);
			BoxCollider col = GetComponent<BoxCollider>();
			if (!col) return;
			Gizmos.DrawCube(transform.position, col.size);
		}
	}
}