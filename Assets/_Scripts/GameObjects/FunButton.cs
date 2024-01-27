using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunButton : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;
    [SerializeField]
    private Sprite _pressedButton;
    [SerializeField]
    private Sprite _unpressedButton;
    [SerializeField]
    private LayerMask _minionLayer;
    [SerializeField]
    private Fun _fun;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((_minionLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) && !_fun.GetIsOn())
        {
            Animator anim = collision.gameObject.GetComponentInChildren<Animator>();
            if (anim.GetInteger("selectedMinion") != 2) return; // Only fat enable the button

            _fun.ToggleOnState();
            _sprite.sprite = _pressedButton;
        }
    }

    private void ResetStates()
    {
        _sprite.sprite = _unpressedButton;
    }

    private void OnEnable()
    {
        LevelManager.ResetGameAction += ResetStates;
    }

    private void OnDisable()
    {
        LevelManager.ResetGameAction -= ResetStates;
    }
}
