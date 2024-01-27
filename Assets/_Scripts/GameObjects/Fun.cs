using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fun : MonoBehaviour
{
    [SerializeField]
    private bool _isOn = false;
    [SerializeField]
    private bool _isBroken = false;
    [SerializeField]
    private Animator _anim;

    private void ResetStates()
    {
        _isOn = false;
        _isBroken = false;
        _anim.SetBool("isOn", false);
    }

    private void OnEnable()
    {
        LevelManager.ResetGameAction += ResetStates;
    }

    private void OnDisable()
    {
        LevelManager.ResetGameAction -= ResetStates;
    }

    private void TurnOnAnimation()
    {
        _anim.SetBool("isOn", true);
    }

    public void ToggleOnState()
    {
        if (_isBroken) return;

        _isOn = !_isOn;
        if (_isOn) TurnOnAnimation();
    }

    public void HasBroken()
    {
        _isBroken = true;
        _isOn = false;
        _anim.SetBool("isOn", false);
    }

    public bool GetIsOn()
    {
        return _isOn && !_isBroken;
    }
}
