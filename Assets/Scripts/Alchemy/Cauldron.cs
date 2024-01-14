using System;
using System.Collections.Generic;
using Alchemy.Ingredients;
using Alchemy.Potions;
using DG.Tweening;
using InteractableSystem;
using PickupableSystem;
using SfxSystem;
using UnityEngine;

namespace Alchemy {
	public class Cauldron : Interactable {
		[SerializeField] private PickupableHolder _holderA;
		[SerializeField] private PickupableHolder _holderB;
		[SerializeField] private PickupableHolder _innerHolderA;
		[SerializeField] private PickupableHolder _innerHolderB;
		[SerializeField] private Transform _pathHelpPoint;
		[SerializeField] private Transform _potionSpawn;
		[SerializeField] private Potion _potionPrefab;
		[SerializeField] private ParticleSystem _createPotionEffect;
		[SerializeField] private PotionMixConfig _mixConfig;

		private Potion _currentPotion;

		public override bool Enabled { get; protected set; } = true;
		
		public override string ActionName => "cauldron";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => Enabled ? InteractionKeyType.Default : InteractionKeyType.None;

		public event Action Mixed;

		public void Add(IngredientType ingredient) {

		}

		public void Remove(IngredientType ingredient) {

		}

		public override void Interact() {
			if(_currentPotion != null || _holderA.CurrentPickupable == null || _holderB.CurrentPickupable == null) return;

			_holderA.TryClaimPickupable(PickupableType.Ingredient, out Pickupable pickupableA);
			_holderB.TryClaimPickupable(PickupableType.Ingredient, out Pickupable pickupableB);

			Vector3[] pathA = {
				Vector3.zero, 
				_innerHolderA.transform.InverseTransformPoint(_pathHelpPoint.position),
				Vector3.up
			};

			Vector3[] pathB = {
				Vector3.zero, 
				_innerHolderB.transform.InverseTransformPoint(_pathHelpPoint.position),
				Vector3.up
			};
			
			_innerHolderA.Pickup(pickupableA, pathA);
			_innerHolderB.Pickup(pickupableB, pathB);
			PotionType potionType = _mixConfig.GetMixResult(pickupableA as Ingredient, pickupableB as Ingredient);

			SfxPlayer.Play(SfxType.Cauldron);
			
			DOTween.Sequence().InsertCallback(1f, () => {
				ResetPickupables();
				CreatePotion(potionType);
			});
		}

		private void ResetPickupables() {
			_innerHolderA.TryClaimPickupable(PickupableType.Ingredient, out Pickupable pickupableA);
			_innerHolderB.TryClaimPickupable(PickupableType.Ingredient, out Pickupable pickupableB);
			
			pickupableA.ResetPickupable();
			pickupableB.ResetPickupable();
		}

		private void CreatePotion(PotionType type) {
			Potion potion = Instantiate(_potionPrefab, _potionSpawn.position, _potionSpawn.rotation);
			potion.Init(type);
			_currentPotion = potion;
			_createPotionEffect.Play();
		}
	}
}