using System;
using UnityEngine;

namespace Ui {
	public abstract class UiMenu : MonoBehaviour {
		protected UiCanvas _uiCanvas;
		public virtual void Init(UiCanvas uiCanvas) => _uiCanvas = uiCanvas;

		public virtual void Show() {
			gameObject.SetActive(true);
		}

		public virtual void Hide() {
			gameObject.SetActive(false);
		}
	}
}