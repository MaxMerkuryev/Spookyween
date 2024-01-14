using UnityEngine;
using Spookyween.Root;
using System.Collections.Generic;

namespace Spookyween.Player.Footsteps {
	[CreateAssetMenu(menuName = Constants.MenuName + "Footsteps/Config")]
	public class FootstepsConfig : ScriptableObject {
		[SerializeField] private List<AudioClip> _clips;
		[SerializeField] private float _pitchShift; 

		[field: SerializeField] public float Period { get; private set; }
		[field: SerializeField] public float PeriodRun { get; private set; }
		[field: SerializeField] public float Pan { get; private set; }

		private int _nextClipIndex;

		public AudioClip GetNextClip() {
			if(_clips.Count == 0) {
				Debug.LogError("Footsteps empty!");	
				return null;
			}

			_nextClipIndex = (_nextClipIndex + 1) % _clips.Count;
			return _clips[_nextClipIndex];
		}

		public float GetPitch() {
			return 1f + Random.Range(-_pitchShift, _pitchShift);
		}
	}
}
