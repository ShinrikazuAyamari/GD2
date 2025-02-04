using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Following this tutorial: https://www.youtube.com/watch?v=RXhTD8YZnY4 (For now) 
public class KnockBackFeedback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;   

    [SerializeField]
    private float strength = 16, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;


    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
