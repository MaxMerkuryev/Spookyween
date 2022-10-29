using System;
using UnityEngine;

namespace Settings {
	[CreateAssetMenu(menuName = "setting")]
	public class Setting : ScriptableObject {
		[SerializeField] private string _name;
		[SerializeField] private float _minValue;
		[SerializeField] private float _maxValue;
		[SerializeField] private float _defaultValue;

		public event Action ValueChanged;
		public float CurrentValue { get; private set; }
		
		public float MinValue => _minValue;
		public float MaxValue => _maxValue;

		public void Awake() {
			CurrentValue = PlayerPrefs.GetFloat(_name, _defaultValue);
		}

		public void SetValue(float value) {
			CurrentValue = Mathf.Clamp(value, _minValue, _maxValue);
			PlayerPrefs.SetFloat(_name, CurrentValue);
			ValueChanged?.Invoke();
		}
	}
}