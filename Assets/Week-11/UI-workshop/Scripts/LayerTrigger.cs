using System;
using UnityEngine;

namespace ctsalidis.Scripts {
    public abstract class LayerTrigger : MonoBehaviour {
        public delegate void TriggerEnterAction(bool isOn);
        public event TriggerEnterAction OnEnterAction;

        private bool isCurrentlyIntrigger = false;

        [SerializeField] private LayerMask _layerMask;

        protected virtual void OnTriggerEnter(Collider other) {
            if (!Utilities.CheckLayer(_layerMask, other.gameObject)) return;
            isCurrentlyIntrigger = true;
            OnEnterAction?.Invoke(isCurrentlyIntrigger);
            var debugInfo = "OnTriggerEnter: " + name + " with " + other.name;
            print(debugInfo);
            UIManager.StatusMessage = debugInfo;
        }

        protected virtual void OnTriggerExit(Collider other) {
            if (!isCurrentlyIntrigger) return;
            isCurrentlyIntrigger = false;
            OnEnterAction?.Invoke(isCurrentlyIntrigger);
            var debugInfo = "OnTriggerExit: " + name + " with " + other.name;
            print(debugInfo);
            UIManager.StatusMessage = debugInfo;
        }
    }
}