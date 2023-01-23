using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public ParticleSystem ps;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null & collision.gameObject.GetComponent<Box>() == null)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 1.5f)
            {
                Destroy(gameObject);
                Instantiate(ps, transform.position, Quaternion.identity);
            }
        }
    }
}