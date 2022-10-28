using UnityEngine;

public class uidisabler : MonoBehaviour {
    [SerializeField] private GameObject _ui;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            _ui.SetActive(!_ui.activeSelf);
        }
    }
}
