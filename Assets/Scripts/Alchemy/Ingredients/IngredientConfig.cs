using Root;
using System;
using UnityEngine;

namespace Alchemy.Ingredients {
	[CreateAssetMenu(menuName = Constants.MenuName + "Ingredient/Config")]
	public class IngredientConfig : ScriptableObject {
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public IngredientType Type { get; private set; }

		public static bool operator ==(IngredientConfig a, IngredientConfig b) => a.Type == b.Type;
		public static bool operator !=(IngredientConfig a, IngredientConfig b) => a.Type != b.Type;

		public static bool operator ==(IngredientConfig a, IngredientType b) => a.Type == b;
		public static bool operator !=(IngredientConfig a, IngredientType b) => a.Type != b;

		public static bool operator ==(IngredientType a, IngredientConfig b) => a == b.Type;
		public static bool operator !=(IngredientType a, IngredientConfig b) => a != b.Type;

		public override bool Equals(object obj) {
			return obj is IngredientConfig config &&
				   base.Equals(obj) &&
				   name == config.name &&
				   hideFlags == config.hideFlags &&
				   Name == config.Name &&
				   Type == config.Type;
		}

		public override int GetHashCode() {
			return HashCode.Combine(base.GetHashCode(), name, hideFlags, Name, Type);
		}
	}

	public enum IngredientType {
		Eyes,
		BatWing,
		Spider,
		WitchsFinger
	}
}
