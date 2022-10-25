using System;
using Alchemy;
using TMPro;
using UnityEngine;

namespace Ui {
	public class PotionEffectUi : MonoBehaviour {
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private RectTransform _fill;
		[SerializeField] private TextMeshProUGUI _potionName;

		private float _initWidth;
		public float current;
		private void Awake() {
			_canvasGroup.alpha = 0f;
			_initWidth = _fill.sizeDelta.x;
		}

		private void OnEnable() {
			PotionEffectController.INSTANCE.OnDrink += OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd += OnPotionEnd;
			PotionEffectController.INSTANCE.OnTimeChange += UpdateFill;
		}

		private void OnDisable() {
			PotionEffectController.INSTANCE.OnDrink -= OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd -= OnPotionEnd;
			PotionEffectController.INSTANCE.OnTimeChange -= UpdateFill;
		}

		private void UpdateFill(float percent) {
			current = percent;
			_fill.sizeDelta = new Vector2(Mathf.Lerp(0f, _initWidth, percent), _fill.sizeDelta.y);
		}

		private void OnPotionDrink(PotionType type) {
			_fill.sizeDelta = new Vector2(_initWidth, _fill.sizeDelta.y);
			_potionName.text = AlchemyData.GetPotionName(type);
			_canvasGroup.alpha = 1f;
		}

		private void OnPotionEnd(PotionType type) {
			_canvasGroup.alpha = 0f;
		}
	}
}