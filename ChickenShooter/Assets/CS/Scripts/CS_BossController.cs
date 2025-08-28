using UnityEngine;

public class CS_BossController : MonoBehaviour
{
    Vector3 direct;
    Animator animator;
    float hp = 100;
    GameObject Ship;
    [SerializeField] GameObject eggBoss;
    void Start()
    {
        direct = Vector3.right;
        animator = GetComponent<Animator>();
        Ship = GameObject.Find("Ship");
        InvokeRepeating("SpawnEgg", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (transform.position.y > 3)
        {
            transform.position += -transform.up * Time.deltaTime;
        }
        else
        {
            transform.position += direct * 3f * Time.deltaTime;
        }
        
    }

    public void TakeDame(int dame)
    {
        hp -= dame;
        animator.SetTrigger("Hurt");
    }

    void SpawnEgg()
    {        
        Instantiate(eggBoss, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            direct = direct == Vector3.right ? Vector3.left : Vector3.right;
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDame(1);
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                Ship.GetComponent<CS_ShipController>().score += 5;
                GetComponent<PolygonCollider2D>().enabled = false;
                Destroy(this.gameObject, 0.5f);
                animator.SetBool("Dead", true);
            }
        }
    }

    //private void OnBecameInvisible() dùng cho viên đạn out camera
    //{
    //    Destroy(gameObject);
    //}

}
