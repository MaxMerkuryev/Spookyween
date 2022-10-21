using System;
using System.Collections.Generic;

namespace Alchemy {
	// im too lazy to use json
	public static class AlchemyData {
		public static Action<PotionType> OnDrinkPotion;
		
		public static string GetPotionName(PotionType type) {
			return type switch {
				PotionType.PumpkinJuice => "pumpkin juice",
				PotionType.VampireBlood => "vampire blood",
				PotionType.GhostBreath => "ghost breath",
				PotionType.Poison => "posion",
				PotionType.WitchsBrew => "witch's brew",
				PotionType.Hypnosis => "hypnosis",
				_ => "wunsch"
			};
		}
		
		public static string GetIngredientName(IngredientType type) {
			return type switch {
				IngredientType.Eyes => "eyes",
				IngredientType.BatWing => "bat wings",
				IngredientType.Spider => "spiders",
				IngredientType.WitchsFinger => "witch's fingers",
				_ => "punsch"
			};
		}

		private static Dictionary<(IngredientType, IngredientType), PotionType> _mixData = new Dictionary<(IngredientType, IngredientType), PotionType>() {
			{(IngredientType.Eyes,		IngredientType.BatWing),		PotionType.PumpkinJuice},
			{(IngredientType.BatWing,	IngredientType.Spider),			PotionType.VampireBlood},
			{(IngredientType.Eyes,		IngredientType.WitchsFinger),	PotionType.GhostBreath},
			{(IngredientType.Eyes,		IngredientType.Spider),			PotionType.Poison},
			{(IngredientType.Spider,	IngredientType.WitchsFinger),	PotionType.WitchsBrew},
			{(IngredientType.BatWing,	IngredientType.WitchsFinger),	PotionType.Hypnosis},
		};

		public static PotionType GetPotion(Ingredient ingredientA, Ingredient ingredientB) {
			if (!ingredientA || !ingredientB) return PotionType.Poison;
			
			bool mix = _mixData.TryGetValue((ingredientA.IngredientType, ingredientB.IngredientType), out PotionType potionType);
			if(!mix) mix = _mixData.TryGetValue((ingredientB.IngredientType, ingredientA.IngredientType), out potionType);
			return mix ? potionType : PotionType.Poison;
		}
	}
	
	public enum PotionType {
		PumpkinJuice,
		VampireBlood,
		GhostBreath,
		Poison,
		WitchsBrew,
		Hypnosis // ♥ I love Tardigrade Inferno ♥
	}

	public enum IngredientType {
		Eyes,
		BatWing,
		Spider,
		WitchsFinger
	}
}