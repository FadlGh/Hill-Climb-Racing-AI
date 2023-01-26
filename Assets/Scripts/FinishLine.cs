using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            StartCoroutine(finish());
        }

        IEnumerator finish()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("UI");
        }
    }
}
