using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    Vector2 move;
    bool isFall;
    bool isJumping;
    float jumpTime;
    int t;
    bool isJump;
    Vector3 savePoint;

    int score;
    public TextMeshProUGUI scoreText;


    public float hp = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        animator.SetFloat("Move",Mathf.Abs(move.x));

        animator.SetBool("isGround", isGround());

        Flip();
        StartCoroutine(Jump());        
        
        if(rb.linearVelocity.y < 0)
        {
            isFall = true;
        }

        if (isGround())
        {
            isFall = false;
            isJumping = false;
        }

        animator.SetBool("Fall",isFall);
        
        StartCoroutine(Die());


    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        if(move.x > 0) transform.localScale = Vector3.one;
        if(move.x < 0) transform.localScale = new Vector3(-1,1,1);
    }
    
    IEnumerator Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            t += 1; Debug.Log(t);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround())
        {

            animator.SetTrigger("Jump");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpTime += Time.deltaTime;
            yield return new WaitForSeconds(0.2f);
            isJump = true;
            yield return new WaitForSeconds(0.8f);
            t = 0;
        }
        if (t > 1 && jumpTime <= 0.5f && isJumping == false)
        {
            t = 0;
            isJumping = true;
            animator.SetTrigger("Double_Jump");
            rb.AddForce(new Vector2(0, 2f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
            jumpTime = 0;
        }
    }

    void TakeDame(float dame)
    {
        hp -= dame;
        
    }

    IEnumerator Die()
    {
        if (hp <= 0)
        {            
            yield return new WaitForSeconds(0.5f);
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }


    bool isGround()
    {
        return Physics2D.OverlapCapsule(groundCheck.position,new Vector2(1f,0.1f),CapsuleDirection2D.Horizontal,0,groundLayer);
    }
    bool isHeadE()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, enemyLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("DeadZone"))
            {
                TakeDame(1);
                if (hp > 0)
                {
                    transform.position = savePoint;
                    
                }

            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (isHeadE())
                {
                    StartCoroutine(collision.gameObject.GetComponent<EnemyTakeDame>().Dame());
                    rb.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
                    score += 5;
                }
                else
                {
                    float direct = (transform.position.x < collision.transform.position.x) ? -1 : 1;
                    rb.AddForce(new Vector2(direct * 5f, 5f), ForceMode2D.Impulse);
                    animator.SetTrigger("Hurt");
                    TakeDame(1);
                }
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("SavePoint"))
            {
                savePoint = transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            score += 1;
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(1f, 0.1f));
    }

}
