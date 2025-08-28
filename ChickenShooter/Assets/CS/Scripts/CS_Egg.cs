using UnityEngine;

public class CS_Egg : MonoBehaviour
{   
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();    
    }
    
    void Update()
    {
        

        if(transform.position.y <= -4.5f)
        {
            animator.SetTrigger("Break");
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);
        }
        else
        {
            transform.position += -transform.up * Time.deltaTime * 3f;
        }
    }
}
