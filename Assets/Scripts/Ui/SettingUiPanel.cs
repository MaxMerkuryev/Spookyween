using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class SettingUiPanel : MonoBehaviour {
		[SerializeField] private Setting _setting;
		[SerializeField] private Slider _slider;
		[SerializeField] private TextMeshProUGUI _minValue;
		[SerializeField] private TextMeshProUGUI _maxValue;
		[SerializeField] private TextMeshProUGUI _currentValue;
		[SerializeField] private string _format;

		public void Init() {
			_minValue.text = _setting.MinValue.ToString(_format);
			_maxValue.text = _setting.MaxValue.ToString(_format);
			_currentValue.text = _setting.CurrentValue.ToString(_format);

			_slider.minValue = _setting.MinValue;
			_slider.maxValue = _setting.MaxValue;
			_slider.value = _setting.CurrentValue;
			
			_slider.onValueChanged.AddListener(UpdateValue);
		}

		private void UpdateValue(float value) {
			_setting.SetValue(value);
			_currentValue.text = value.ToString(_format);
		}
	}
}