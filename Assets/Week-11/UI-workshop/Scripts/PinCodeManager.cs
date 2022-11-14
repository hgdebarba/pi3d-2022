using TMPro;
using UnityEngine;

namespace ctsalidis.Scripts {
    public class PinCodeManager : MonoBehaviour {
        [SerializeField] private TMP_Text _text;
        private string CodeStatus = string.Empty;
        [SerializeField] private string correctPin = "1123";
    
        public delegate void PinCodeCorrectAction();

        public static event PinCodeCorrectAction OnPinCodeCorrect;

        public void AddToPinCode(string number) {
            if (string.IsNullOrEmpty(CodeStatus)) CodeStatus = number;
            else CodeStatus += number;
            _text.text = CodeStatus;
            print("Added number");
        
            // check if the pin code added is correct
            // (for now, just check if it is of a certain character length)
            if(CodeStatus == correctPin) OnPinCodeCorrect?.Invoke();
            else if(CodeStatus.Length >= 4) ClearCode();
        }

        private void ClearCode() {
            print("Clear code");
            _text.text = "Add code";
            CodeStatus = string.Empty;
        }
    }
}
