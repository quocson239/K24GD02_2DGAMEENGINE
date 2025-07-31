using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class FBPlayer : MonoBehaviour
{
    float jumpForce = 5f;
    Rigidbody2D rb;
    Animator animator;
    public AudioSource flySound;
    public AudioSource hitSound;
    public AudioSource dieSound;
    public AudioSource pointSound;
    public GameObject canvas;
    public GameObject cam;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fly();
        }
        //Nghiêng người
        float angle = Mathf.Clamp(rb.linearVelocity.y * 3f, -90f, 30f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Fly()
    {
        rb.AddForce(new Vector3(0, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("fly");
        flySound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Die());

            Debug.Log("Died");
            
            hitSound?.Play();
         
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Point"))
        {
            Debug.Log("+ 1");
            pointSound?.Play();
            canvas.GetComponent<FBCava>().currentPoint += 1;
        }
    }

    int n = 5;
    void ShakeCam()
    {
        n = (n == 5) ? -5 : 5; 
        cam.transform.rotation = Quaternion.Euler(0,0,n);
    }    
    IEnumerator Die()
    {
        InvokeRepeating("ShakeCam", 0, 0.025f);
        yield return new WaitForSeconds(0.1f);
        CancelInvoke();
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        Time.timeScale = 0;
    }    


}
