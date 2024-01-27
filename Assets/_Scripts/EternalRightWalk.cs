using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternalRightWalk : MonoBehaviour
{
    [Header("Movement props")]
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _force;

    [Header("Bouncing props")]
    [SerializeField]
    private LayerMask _wallLayer;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _sprites;
    [SerializeField]
    private Animator _animationController;

    private bool _isFacingRight = true;
    private bool _isRunningWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_animationController.GetBool("isDead"))
        {
            DeadState();
            return;
        }

        if ((_wallLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            transform.rotation = Quaternion.Euler(0, _isFacingRight ? 0 : 180, 0);
            _sprites.rotation = Quaternion.Euler(0, !_isFacingRight ? 0 : 180, 0);
            _isFacingRight = !_isFacingRight;
        }

        if (((_groundLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) && !_isRunningWalk)
        {
            _animationController.SetBool("isRunning", true);
            _isRunningWalk = true;
        }
    }

    private void FixedUpdate()
    {
        if (_animationController.GetBool("isDead"))
        {
            DeadState();
            return;
        }

        if (_isRunningWalk)
        {
            Vector2 dir = _isFacingRight ? Vector2.right : Vector2.left;
            _rb.velocity = new Vector2((dir * _force).x, _rb.velocity.y);
        }
    }

    private void DeadState()
    {
        _force = 0f;
        _rb.velocity = Vector2.zero;
    }

    public void SetForce(float force)
    {
        _force = force;
    }
}
