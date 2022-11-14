using System;
using JetBrains.Annotations;
using UnityEngine;

namespace ctsalidis.Scripts {
    public class UIManager : MonoBehaviour {
        private bool showDebugInfo = false;
        private bool showHint = false;

        [SerializeField] private GameObject hintUI;

        private static string _statusMessage = string.Empty;
        public static string StatusMessage {
            get {
                if(string.IsNullOrEmpty(_statusMessage)) StatusMessage = "No status message";
                return _statusMessage;
            }
            set => _statusMessage = "LatestStatus: " + value;
        }

        private void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 25), "Debug Info")) {
                print("You clicked the debug info button!");
                showDebugInfo = !showDebugInfo; // toggle the value of the boolean
            }

            if (showDebugInfo && !string.IsNullOrEmpty(StatusMessage)) {
                GUI.Label(new Rect(10, 40, Screen.width - 20, 100), StatusMessage);
                
                if(GUI.Button(new Rect(10, Screen.height - 50, 150, 25), "Show pin code hint?")) {
                    showHint = !showHint;
                    ShowHintUI();
                }
            }
        }

        private void ShowHintUI() => hintUI.SetActive(showHint);
    }
}