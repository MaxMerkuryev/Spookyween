using DG.Tweening;
using UnityEngine;

namespace Wispers {
	public class WallActivatable : MonoBehaviour {
		public void Activate() {
			transform.DOMoveY(-15f, 6f).OnComplete(() => {
				gameObject.SetActive(false);
			});
		}
	}
}