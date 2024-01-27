using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action GetTheGoal;

    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _goalLayer;
    [SerializeField]
    private LayerMask _funLayer;
    [SerializeField]
    private LayerMask _portalLayer;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private EternalRightWalk _walkScript;

    private EMinionState _kindOf;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_funLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Fun fun = collision.gameObject.GetComponent<Fun>();
            if (fun.GetIsOn() && _animator.GetInteger("selectedMinion") != 1)
            {
                fun.HasBroken();
                _animator.SetBool("isDead", true);
                _walkScript.SetForce(0f);
            }
        }

        if ((_playerLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Physics2D.IgnoreCollision(
                collision.gameObject.GetComponent<Collider2D>(),  // Another minion
                GetComponent<Collider2D>() // Current minion
            );
        }

        if ((_goalLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            GetTheGoal();
        }
    }

    public EMinionState GetMinionType()
    {
        return _kindOf;
    }

    public void SetMinionType(EMinionState kind)
    {
        _kindOf = kind;
    }
}
