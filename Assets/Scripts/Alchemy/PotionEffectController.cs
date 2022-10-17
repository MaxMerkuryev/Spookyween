using System;
using UnityEngine;

namespace Alchemy {
	public class PotionEffectController : MonoBehaviour {
		public static PotionEffectController INSTANCE;
		
		public event Action<PotionType> OnDrink;
		public event Action<PotionType> OnEnd;
		public event Action<float> OnTimeChange;

		private bool _workin;
		private float _currentTime;
		private PotionType _currentPotionType;

		private float CurrentTime {
			get => _currentTime;
			set {
				_currentTime = value;
				OnTimeChange?.Invoke(_currentTime);
			}
		}

		private void Awake() {
			INSTANCE = this;
		}

		public void Drink(PotionType type) {
			if (_workin) End();
			_currentPotionType = type;
			CurrentTime = GetPotionTime(type);
			OnDrink?.Invoke(type);
		}

		private void End() {
			_workin = false;
			CurrentTime = 0;
			OnEnd?.Invoke(_currentPotionType);
		}
		
		private void Update() {
			if (CurrentTime <= 0f) {
				CurrentTime = 0f;
				if (_workin) {
					End();
					return;
				}
			}

			CurrentTime -= Time.deltaTime;
		}

		private float GetPotionTime(PotionType type) {
			return type switch {
				PotionType.PumpkinJuice => 5f,
				PotionType.VampireBlood => 30f,
				PotionType.GhostBreath => 30f,
				PotionType.Poison => 5f,
				PotionType.WitchsBrew => 15f,
				PotionType.Hypnosis => 15f,
				_ => 0f
			};
		}
	}
}