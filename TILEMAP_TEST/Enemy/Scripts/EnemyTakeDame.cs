using System.Collections;
using UnityEngine;

public class EnemyTakeDame : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Dame()
    {
        animator.SetTrigger("Hurt");
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject, 1f);
    }
}
