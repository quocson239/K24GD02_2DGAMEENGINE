using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    int i = 1;
    Coroutine Running;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Running = StartCoroutine(WalkA());        
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {        

    }

    

    IEnumerator WalkA()
    {
        i = -i;
        transform.localScale = new Vector3(i, 1, 1);
        animator.SetBool("Walk", true);

        rb.linearVelocity = new Vector2(i * 3f, 0);
        
        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = new Vector2(i * 3f, 0);        

        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = new Vector2(i * 3f, 0);        

        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = new Vector2(i * 3f, 0);

        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = new Vector2(i * 3f, 0);

        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = new Vector2(i * 3f, 0);

        yield return new WaitForSeconds(0.5f);

        StopCoroutine(Running);
        Running = StartCoroutine(StandA());
    }

    IEnumerator StandA()
    {
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetBool("Walk", false);

        yield return new WaitForSeconds(2f);

        StopCoroutine(Running);
        Running = StartCoroutine(WalkA());
    }

}
