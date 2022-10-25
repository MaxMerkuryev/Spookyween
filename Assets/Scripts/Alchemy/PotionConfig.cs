using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alchemy {
	[CreateAssetMenu(menuName = "potion config")]
	public class PotionConfig : ScriptableObject {
		[SerializeField] private List<PotionData> _data;

		public PotionData GetPotionData(PotionType potionType) {
			return _data.FirstOrDefault(x => x.PotionType == potionType);
		}
	}
	
	[Serializable]
	public class PotionData {
		public PotionType PotionType;
		public Material Material;
		public Color ParticleColor;
	}
}