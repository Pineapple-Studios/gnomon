using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenGround : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Animator anim = collision.gameObject.GetComponentInChildren<Animator>();
        if (anim != null && anim.GetInteger("selectedMinion") == 2)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.freezeRotation = true;
        }
    }
}
