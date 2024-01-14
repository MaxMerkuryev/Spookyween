using DG.Tweening;
using UnityEngine;

namespace Alchemy.Ingredients {
	// todo: move into generic animation component
	public class EyeIngredientAnimation : MonoBehaviour {
		private void Awake() => Rotate();
		private void Rotate() => transform
			.DOLocalRotate(new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f), Random.Range(.1f, 3f))
			.SetEase(Ease.OutBack)
			.OnComplete(Rotate);
	}
}