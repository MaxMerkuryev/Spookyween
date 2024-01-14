using Alchemy.Ingredients;
using PickupableSystem;
using UnityEngine;

namespace Alchemy {
	public class Ingredient : Pickupable {
		[SerializeField] private IngredientConfig _config;
		
		public override string Name => _config.Name;
		public override PickupableType Type => PickupableType.Ingredient;

		protected override Vector3 _customOrientation => new Vector3(90f, 0f, 0f);
	}
}