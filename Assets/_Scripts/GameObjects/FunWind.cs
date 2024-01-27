using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunWind : MonoBehaviour
{
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private float _windSpeed = 5f;

    private Fun _fun;
    private List<Rigidbody2D> _rbs = new List<Rigidbody2D> { };

    private void ResetStates()
    {
        _rbs.Clear();
    }

    private void OnEnable()
    {
        LevelManager.ResetGameAction += ResetStates;
    }

    private void OnDisable()
    {
        LevelManager.ResetGameAction -= ResetStates;
    }

    private void Start()
    {
        _fun = GetComponentInParent<Fun>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            (_playerLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer 
            && _fun.GetIsOn()
        )
        {
            _rbs.Add(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (
            (_playerLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer
            && _fun.GetIsOn()
        )
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            _rbs.Remove(rb);
        }
    }

    private void FixedUpdate()
    {
        if (_rbs.Count > 0 && _fun.GetIsOn())
        {
            foreach(Rigidbody2D _rb in _rbs)
            {
                Animator anim = _rb.gameObject.GetComponentInChildren<Animator>();
                // Debug.Log(anim.GetInteger("selectedMinion"));
                if (anim.GetInteger("selectedMinion") == 1) // Thin minion
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _windSpeed);
                }
            }
        }
    }

    
}
