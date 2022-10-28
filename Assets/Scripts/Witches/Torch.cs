using PickupableSystem;
using UnityEngine;

namespace Witches {
	public class Torch : Pickupable {
		[SerializeField] private ParticleSystem _fire;
		[SerializeField] private Light _light;
		[SerializeField] private AudioSource _audioSource;

		public override string Name => "torch";
		public override PickupableType Type => PickupableType.Torch;
		public bool IsActive { get; private set; }
		protected override Vector3 _customOrientation => new Vector3(-80f, 0f, 0f);

		public void Activate() {
			_fire.Play();
			_audioSource.Play();
			_light.enabled = true;
			IsActive = true;
		}

		public void Deactivate() {
			_audioSource.Stop();
			_fire.Stop();
			_light.enabled = false;
			IsActive = false;
		}
	}
}