using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public LayerMask mask;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != mask)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 2f)
            {
                Destroy(gameObject);
            }
        }
    }
}

