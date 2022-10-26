using System;
using System.Linq;
using DG.Tweening;
using SfxSystem;
using UnityEngine;

namespace Pumpkins {
	public class PumpkinController : MonoBehaviour {
		[SerializeField] private float duration = 10f;
		[SerializeField] private ParticleSystem _finishParticles;
		[SerializeField] private ParticleSystem _portalAppearParticles;
		[SerializeField] private Transform _portal;
		[SerializeField] private PumpkinHolder[] _holders;

		private void Awake() {
			foreach (PumpkinHolder pumpkinHolder in _holders) {
				pumpkinHolder.OnPickup += OnPickup;
			}
		}

		private void OnPickup() {
			if (_holders.Any(t => !t.Correct)) {
				return;
			}

			foreach (var h in _holders) {
				h.Disable();
			}
			
			Finish();
		}

		private void Finish() {
			Vector3 initScale = _portal.localScale;
			_portal.localScale = Vector3.zero;

			_finishParticles.Play();
			SfxPlayer.Play(SfxType.PortalFinish);
			DOTween.Sequence().InsertCallback(duration, ()=> {
				_portalAppearParticles.Play();
				_portal.gameObject.SetActive(true);
				_finishParticles.Stop();
				SfxPlayer.Play(SfxType.Thunder);
				_portal.DOScale(initScale, 0.5f).SetEase(Ease.OutCirc);
			}); 
		}

		private void Reset() {
			_portal.gameObject.SetActive(false);
		}
		
		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				Reset();
			}
			if (Input.GetKeyDown(KeyCode.T)) {
				Finish();
			}
		}
	}
}