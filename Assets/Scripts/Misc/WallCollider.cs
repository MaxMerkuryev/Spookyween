using System;
using UnityEngine;

namespace Misc {
	public class WallCollider : MonoBehaviour {
		private void OnDrawGizmos() {
			Gizmos.color = new Color(0,1,0,.1f);
			Gizmos.DrawCube(transform.position, transform.localScale);
		}
	}
}