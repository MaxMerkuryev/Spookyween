using PickupableSystem;
using UnityEngine;

namespace Alchemy {
	public class Ingredient : Pickupable {
		[SerializeField] private IngredientType _ingredientType;
		public IngredientType IngredientType => _ingredientType;
		
		public override string Name => AlchemyData.GetIngredientName(_ingredientType);
		public override PickupableType Type => PickupableType.Ingredient;

		protected override Vector3 _customOrientation => new Vector3(90f, 0f, 0f);
	}
}