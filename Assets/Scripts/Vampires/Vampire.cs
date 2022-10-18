using UnityEngine;

namespace Vampires {
	public class Vampire : MonoBehaviour {
		[SerializeField] private VampireEyeball[] _eyes;
		[SerializeField] private ParticleSystem _blood;

		public bool Dead { get; private set; }
		
		public void Die() {
			Dead = true;
			_blood.Play();
			foreach (VampireEyeball eye in _eyes) eye.SetDead();
		}

		private void Awake() {
			foreach (VampireEyeball eye in _eyes) eye.SetDefault();
		}
	}
}