using Player;
using UnityEngine;

namespace Wispers {
	public class WallActivator : MonoBehaviour {
		[SerializeField] private WallActivatable wallActivatable;
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out PlayerController p)) {
				wallActivatable.Activate();
				enabled = false;
			}
		}
	}
}