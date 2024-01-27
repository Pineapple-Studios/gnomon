using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerZone : MonoBehaviour
{
    [SerializeField]
    private LayerMask _minionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & _minionLayer) != 0))
        {
            Destroy(collision.gameObject);
        }
    }
}
