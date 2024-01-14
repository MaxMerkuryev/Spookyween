using Alchemy;
using CommonPuzzle;
using UnityEngine;

namespace Vampires {
	public class VampirePuzzle : PuzzleBase {
		[SerializeField] private PuzzleTarget _target;
		[SerializeField] private Vampire[] _vampires;

		protected override PotionType _potionType => PotionType.VampireBlood;
		private int _count = 0;
		
		private void Awake() {
			foreach (Vampire vampire in _vampires) {
				vampire.Init(this);
			}
		}

		public void OnKillVampire() {
			_count++;
			if (_count >= _vampires.Length) {
				_target.Activate();
			}
		}
	}
}