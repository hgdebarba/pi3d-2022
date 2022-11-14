using System;
using System.Collections;
using System.Collections.Generic;
using ctsalidis.Scripts;
using UnityEngine;

public class DoorManager : MonoBehaviour {
    [SerializeField] private Animator _animator;

    private void OnEnable() => PinCodeManager.OnPinCodeCorrect += PinCodeCorrect;
    private void OnDisable() => PinCodeManager.OnPinCodeCorrect -= PinCodeCorrect;

    private void PinCodeCorrect() {
        _animator.Play("DoorOpen");
        UIManager.StatusMessage = "Pin code is correct, the door can open.";
    }
}
