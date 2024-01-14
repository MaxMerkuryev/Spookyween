using Alchemy;
using CommonPuzzle;
using UnityEngine;

namespace Witches {
	public class WitchsPuzzle : PuzzleBase {
		[SerializeField] private PuzzleTarget _target;
		[SerializeField] private Witch[] _witches;

		protected override PotionType _potionType => PotionType.WitchsBrew;

		private int count = 0;
		
		private void Awake() {
			foreach (Witch witch in _witches) {
				witch.Init(this);
			}
		}

		public void OnWitchFire() {
			count++;
			if (count >= _witches.Length) {
				_target.Activate();
			}
		}
	}
}