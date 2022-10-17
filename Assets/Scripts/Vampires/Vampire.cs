using UnityEngine;

namespace Vampires {
	public class Vampire : MonoBehaviour{
		[SerializeField] private VampireEyeball[] _eyes;

		public void Die() {
			foreach (VampireEyeball eye in _eyes) eye.SetDead();
		}

		private void Awake() {
			foreach (VampireEyeball eye in _eyes) eye.SetDefault();
		}
	}
}