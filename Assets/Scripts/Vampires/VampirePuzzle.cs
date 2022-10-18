using System;
using Alchemy;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vampires {
	public class VampirePuzzle : PotionEffectReceiver {
		[SerializeField] private VampirePuzzleTarget _target;
		[SerializeField] private VampireCoffin[] _coffins;

		protected override PotionType _potionType => PotionType.VampireBlood;

		private float _offset => Random.Range(0.01f, 0.05f);
		private int _count = 0;
		
		private void Awake() {
			foreach (VampireCoffin coffin in _coffins) {
				coffin.Init(OnKillVampire);
			}
		}

		protected override void OnPotionDrinkAction() {
			Sequence sequence = DOTween.Sequence();
			for (int i = 0; i < _coffins.Length; i++) {
				VampireCoffin vampireCoffin = _coffins[i];
				sequence.InsertCallback(i * _offset, vampireCoffin.Open);
			}
		}

		protected override void OnPotionEndAction() {
			Sequence sequence = DOTween.Sequence();
			for (int i = 0; i < _coffins.Length; i++) {
				VampireCoffin vampireCoffin = _coffins[i];
				sequence.InsertCallback(i * _offset, vampireCoffin.Close);
			}
		}

		private void OnKillVampire() {
			_count++;
			if (_count >= _coffins.Length) {
				_target.Open();
			}
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				OnPotionDrinkAction();
			}
			if (Input.GetKeyDown(KeyCode.T)) {
				OnPotionEndAction();
			}
		}
	}
}