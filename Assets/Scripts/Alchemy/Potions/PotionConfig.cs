using Root;
using UnityEngine;

namespace Alchemy.Potions {
	[CreateAssetMenu(menuName = Constants.MenuName + "Potion/Config")]
	public class PotionConfig : ScriptableObject {
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public PotionType Type { get; private set; }
		[field: SerializeField] public Material Material { get; private set; }
		[field: SerializeField] public Color Color { get; private set; }
	}

	public enum PotionType {
		PumpkinJuice,
		VampireBlood,
		GhostBreath,
		Poison,
		WitchsBrew,
		Hypnosis // ♥ I love Tardigrade Inferno ♥
	}
}
