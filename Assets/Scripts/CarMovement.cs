using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{  
    public GameObject frontTire;
    public GameObject backTire;

    private Rigidbody2D frontTireRb;
    private Rigidbody2D backTireRb;
    private Rigidbody2D carRb;

    private float movement;
    public float speed;
    public float maxSpeed;
    public float flipForce;

    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        frontTireRb = frontTire.GetComponent<Rigidbody2D>();
        backTireRb = backTire.GetComponent<Rigidbody2D>();
        carRb = GetComponent<Rigidbody2D>();  
    }
    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
    }

    private bool isGrounded(GameObject obj)
    {
        return Physics2D.OverlapCircle(obj.transform.position, 0.3f, ground);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(frontTireRb.angularVelocity) < maxSpeed & Mathf.Abs(backTireRb.angularVelocity) < maxSpeed)
        {
            frontTireRb.AddTorque(-movement * speed * Time.fixedDeltaTime);
            backTireRb.AddTorque(-movement * speed * Time.fixedDeltaTime);
            if (!isGrounded(frontTire) & !isGrounded(backTire))
            {
                frontTireRb.AddForce(flipForce * -movement * Vector2.down);
                backTireRb.AddForce(flipForce * movement * Vector2.down);
            }
            carRb.AddForce(Vector2.right * 5 * movement);
        }
    }
}
