using System.Collections.Generic;
using DG.Tweening;
using InteractableSystem;
using PickupableSystem;
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

		private Potion _currentPotion;
		
		public override bool Enabled {
			get => _currentPotion == null && _holderA.CurrentPickupable != null && _holderB.CurrentPickupable != null;
			protected set {}
		}

		public override string ActionName => "make a potion";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => Enabled ? InteractionKeyType.Default : InteractionKeyType.None;
		
		public override void Interact() {
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
			PotionType potionType = AlchemyData.GetPotion(pickupableA as Ingredient, pickupableB as Ingredient);

			Debug.LogError($"{(pickupableA as Ingredient).IngredientType} + {(pickupableB as Ingredient).IngredientType} = {potionType}");
			
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
		}
	}
}