using System;
using System.Collections.Generic;
using UnityEngine;

namespace SfxSystem {
	[CreateAssetMenu(menuName = "sfx config")]
	public class SfxConfig : ScriptableObject {
		[SerializeField] private List<SfxPair> _clips;
		
		public AudioClip GetClip(SfxType sfxType) {
			foreach (SfxPair pair in _clips) {
				if(pair.Type != sfxType) continue;
				return pair.GetClip();
			}

			return null;
		}

		// for inspector
		private void OnValidate() {
			foreach (SfxPair pair in _clips) {
				pair.SetName();
			}
		}
	}

	[Serializable]
	public class SfxPair {
		public SfxType Type;
		[SerializeField] private AudioClip[] _clips;

		private int _index;
		
		public AudioClip GetClip() {
			_index = (_index + 1) % _clips.Length;
			return _clips[_index];
		}
		
		// for inspector
		[HideInInspector] public string name;
		public void SetName() {
			name = Type.ToString();
		}
	}
	
	public enum SfxType {
		Foot,
		Pickup,
		Drop
	}
}