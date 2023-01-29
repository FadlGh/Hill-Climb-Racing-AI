using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinishLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            StartCoroutine(finish());
        }

        IEnumerator finish()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("UI");
        }
    }
}
