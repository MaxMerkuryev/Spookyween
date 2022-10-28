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
				return pair.Clip;
			}

			return null;
		}
	}

	[Serializable]
	public class SfxPair {
		public SfxType Type;
		public AudioClip Clip;
	}
	
	public enum SfxType {
		Pickup,
		Drop,
		Jump,
		PortalFinish,
		Thunder,
		PotionDrink,
		PotionEnd,
		WispTrigger,
		PuzzleTargetActivate,
		VampireDie,
		
	}
}