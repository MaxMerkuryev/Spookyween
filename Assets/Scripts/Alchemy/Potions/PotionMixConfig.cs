using System.Collections.Generic;
using UnityEngine;

using Root;
using Alchemy.Ingredients;

namespace Alchemy.Potions {
	[CreateAssetMenu(menuName = Constants.MenuName + "Potion/MixConfig")]
	public class PotionMixConfig : ScriptableObject {
		[SerializeField] private PotionConfig _defaulResult;
		[SerializeField] private List<PotionMixConfigItem> _items;

		public PotionConfig GetMixResult(IngredientType a, IngredientType b) {
			PotionMixConfigItem item = _items.Find(x => (x.A == a && x.B == b) || (x.A == b && x.B == a));
			return item != null ? item.Result : _defaulResult;
		}
	}

	public class PotionMixConfigItem {
		[field: SerializeField] public IngredientConfig A { get; private set; }
		[field: SerializeField] public IngredientConfig B { get; private set; }
		[field: SerializeField] public PotionConfig Result { get; private set; }
	}
}
