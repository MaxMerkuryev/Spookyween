using UnityEngine;

namespace Vampires {
	public class VampireEyeball : MonoBehaviour {
		[SerializeField] private GameObject _default;
		[SerializeField] private GameObject _dead;

		public void SetDefault() {
			_default.SetActive(true);
			_dead.SetActive(false);
		}

		public void SetDead() {
			_default.SetActive(false);
			_dead.SetActive(true);
		}
	}
}