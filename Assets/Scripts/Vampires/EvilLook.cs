using System;
using Player;
using UnityEngine;

namespace Vampires {
	public class EvilLook : MonoBehaviour {
		private void Update() {
			transform.LookAt(PlayerController.CAMERA_POSITION);
		}
	}
}