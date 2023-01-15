using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    public GameObject frontTire;
    public GameObject backTire;
    public GameObject car;

    private Rigidbody2D frontTireRb;
    private Rigidbody2D backTireRb;
    private Rigidbody2D carRb;

    public float speed;
    public float maxSpeed;
    public float flipForce;

    public Transform leftPoint;
    public Transform rightPoint;
    private int direction;

    private RaycastHit2D[] rightHit;
    private int rightCounter;
    private RaycastHit2D[] leftHit;
    private int leftCounter;
    private bool needReturn;

    // Start is called before the first frame update
    void Start()
    {
        frontTireRb = frontTire.GetComponent<Rigidbody2D>();
        backTireRb = backTire.GetComponent<Rigidbody2D>();
        carRb = car.GetComponent<Rigidbody2D>();

        rightHit = new RaycastHit2D[3];
        leftHit = new RaycastHit2D[3];
    }

    private bool isStuck()
    {
        return Mathf.Abs(carRb.velocity.x) < 1.2f;
    }

    private void directionFunction()
    {
        rightHit[0] = Physics2D.Raycast(rightPoint.position, new Vector3(1, 0, 0));
        rightHit[1] = Physics2D.Raycast(rightPoint.position, new Vector3(1, 0.5f, 0));
        rightHit[2] = Physics2D.Raycast(rightPoint.position, new Vector3(1, -1, 0));

        leftHit[0] = Physics2D.Raycast(leftPoint.position, new Vector3(-1, 0, 0));
        leftHit[1] = Physics2D.Raycast(leftPoint.position, new Vector3(-1, 0.5f, 0));
        leftHit[2] = Physics2D.Raycast(leftPoint.position, new Vector3(-1, -1, 0));

        for (int i = 0; i < rightHit.Length; i++)
        {
            if (rightHit[i])
            {
                rightCounter++;
            }
        }

        for (int j = 0; j < leftHit.Length; j++)
        {
            if (leftHit[j])
            {
                leftCounter++;
            }
        }

        if (rightCounter > leftCounter)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        /*
        Sometimes the AI get stuck even when switching direction so I checked
        the up-right diagonal raycast and if its true the AI move a little backward 
        to gain momentum then accelerate.
        */

        if (rightHit[1])
        {
            StartCoroutine(MoveBackward());
        }
    }

    private IEnumerator MoveBackward()
    {
        needReturn = true;
        yield return new WaitForSeconds(3f);
        needReturn = false;
    }

    private bool isFlipped()
    {
        return car.transform.rotation.z > 90 & car.transform.rotation.z < -90;
    }

    void FixedUpdate()
    {
        directionFunction();

        if (Mathf.Abs(frontTireRb.angularVelocity) < maxSpeed & Mathf.Abs(backTireRb.angularVelocity) < maxSpeed)
        {
            if (isFlipped())
            {
                car.transform.Rotate(new Vector3(0, 0, 0));
 
            }
            if (isStuck()) 
            {
                if (needReturn)
                {
                    frontTireRb.AddTorque(-speed * Time.fixedDeltaTime);
                    backTireRb.AddTorque(-speed * Time.fixedDeltaTime);
                }
                frontTireRb.AddTorque(direction * speed * Time.fixedDeltaTime);
                backTireRb.AddTorque(direction * speed * Time.fixedDeltaTime);
                
                carRb.AddForce(Vector2.right * 5);
            }
            if(!isStuck() & !isFlipped())
            {
                carRb.AddForce(5 * direction * Vector2.right);

                frontTireRb.AddTorque(-1 * speed * Time.fixedDeltaTime);
                backTireRb.AddTorque(-1 * speed * Time.fixedDeltaTime);
            }       
        }
        Debug.DrawRay(rightPoint.position, new Vector3(1, 0, 0), Color.red);
        Debug.DrawRay(rightPoint.position, new Vector3(1, 0.5f, 0), Color.red);
        Debug.DrawRay(rightPoint.position, new Vector3(1, -1, 0), Color.red);
    }
}
