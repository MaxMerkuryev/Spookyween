using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Alchemy {
	public class PotionEffectController : MonoBehaviour {
		[SerializeField] private VolumeProfile _postProcessing;
		[SerializeField] private List<PotionEffect> _effects = new List<PotionEffect>();
		
		public static PotionEffectController INSTANCE;
		
		public event Action<PotionType> OnDrink;
		public event Action<PotionType> OnEnd;
		public event Action<float> OnTimeChange;

		private bool _workin;
		private float _currentTime;
		private PotionEffect _currentPotionEffect;
		private Vignette _colorAdjustments;		
		
		private float CurrentTime {
			get => _currentTime;
			set {
				_currentTime = value;
				OnTimeChange?.Invoke(_currentTime / _currentPotionEffect.Duration);
			}
		}

		private void Awake() {
			INSTANCE = this;
			_postProcessing.TryGet(out _colorAdjustments);
			_colorAdjustments.color.Override(Color.white);
		}

		private void OnDestroy() {
			_colorAdjustments.color.Override(Color.white);
		}

		public void Drink(PotionType type) {
			if (_workin) End();
			_workin = true;
			_currentPotionEffect = _effects.FirstOrDefault(e => e.Type == type);
			CurrentTime = _currentPotionEffect.Duration;
			DOVirtual.Color(_colorAdjustments.color.value, _currentPotionEffect.ColorFilter, 1f, color => {
				_colorAdjustments.color.Override(color);
			}).SetEase(Ease.OutCirc);
			OnDrink?.Invoke(type);
		}

		private void End() {
			_workin = false;
			CurrentTime = 0;
			DOVirtual.Color(_colorAdjustments.color.value, Color.white, 1f, color => {
				_colorAdjustments.color.Override(color);
			}).SetEase(Ease.OutCirc);
			OnEnd?.Invoke(_currentPotionEffect.Type);
		}
		
		private void Update() {
			if(!_workin) return;
			if (CurrentTime <= 0f) {
				CurrentTime = 0f;
				if (_workin) {
					End();
					return;
				}
			}

			CurrentTime -= Time.deltaTime;
		}
	}

	[Serializable]
	public struct PotionEffect {
		public PotionType Type;
		public float Duration;
		
		[ColorUsage(true, true)]
		public Color ColorFilter;
	}
}