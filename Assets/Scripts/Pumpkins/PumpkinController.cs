using System;
using System.Linq;
using UnityEngine;

namespace Pumpkins {
	public class PumpkinController : MonoBehaviour {
		[SerializeField] private PumpkinHolder[] _holders;

		private void Awake() {
			foreach (PumpkinHolder pumpkinHolder in _holders) {
				pumpkinHolder.OnPickup += OnPickup;
			}
		}

		private void OnPickup() {
			if (_holders.Any(pumpkinHolder => pumpkinHolder.CurrentPickupable == null)) {
				return;
			}
			
			Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
		}
	}
}