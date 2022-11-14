using System;
using UnityEngine;

namespace ctsalidis.Scripts {
    public class LightBulbTrigger : LayerTrigger {
        [SerializeField] private GameObject light;
        
        private void Start() => light.gameObject.SetActive(false);

        protected override void OnTriggerEnter(Collider other) {
            base.OnTriggerEnter(other);
            light.gameObject.SetActive(true);
            UIManager.StatusMessage = "User is inside trigger. Turn lightbulb ON";
            Cursor.lockState = CursorLockMode.None;
        }

        protected override void OnTriggerExit(Collider other) {
            base.OnTriggerExit(other);
            light.gameObject.SetActive(false);
            UIManager.StatusMessage = "User is outside trigger. Turn lightbulb OFF";
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
