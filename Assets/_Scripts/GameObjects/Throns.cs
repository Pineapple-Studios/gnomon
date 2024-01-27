using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throns : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator anim = collision.gameObject.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetBool("isDead", true);
        }
    }
}
