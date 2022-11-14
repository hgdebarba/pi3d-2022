using System;
using System.Collections;
using System.Collections.Generic;
using ctsalidis.Scripts;
using TMPro;
using UnityEngine;

public class PinNumber : MonoBehaviour {
    [SerializeField] private string number = 0.ToString();
    [SerializeField] private PinCodeManager _pinCodeManager;

    private TMP_Text _text;

    [SerializeField] private Vector3 offset;
    private Animator _animator;
    private bool animPlayedAtLeastOneTime = false;

    private void Start() {
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = number;
        _animator = GetComponent<Animator>();
        offset.x = transform.position.x;
        offset.y = transform.position.y;
    }

    private void LateUpdate() {
        if (animPlayedAtLeastOneTime) {
            transform.position = new Vector3(offset.x, offset.y, transform.position.z);
        }
    }

    public void AddNumberToPin() {
        PlayAnimation();
        _pinCodeManager.AddToPinCode(number);
        UIManager.StatusMessage = "Number " + number + " added to pin code";
    }

    private void PlayAnimation() {
        animPlayedAtLeastOneTime = true;
        _animator.Play("PinNumber");
    }
}
