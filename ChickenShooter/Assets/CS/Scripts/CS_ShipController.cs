using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CS_ShipController : MonoBehaviour
{
    [SerializeField] GameObject[] bulletPrefab;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject firePoint;
    [SerializeField] Text scoreText;

    [SerializeField] Image[] hearts;
    float hp = 3;
    float attackCooldown = 0.3f;
    float attackTime;
    public int powerLv = 0;
    public int score;
    bool isImmute;
    Animator animator;
    void Start()
    {        
        animator = GetComponent<Animator>();
        scoreText.text = score.ToString();
    }
    
    void Update()
    {
        Move();
        Attack();
        UpdateHp();
        UpdateScore();
        shield.gameObject.SetActive(isImmute);
        GetComponent<PolygonCollider2D>().enabled = !isImmute;
    }


    void Move()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        
        mouseWorldPosition.z = 0;

        // Giới hạn vị trí trong vùng nhất định
        float minX = -7f; // Giới hạn bên trái
        float maxX = 7f;  // Giới hạn bên phải
        float minY = -4f; // Giới hạn dưới
        float maxY = 4f;  // Giới hạn trên

        // Giới hạn vị trí
        mouseWorldPosition.x = Mathf.Clamp(mouseWorldPosition.x, minX, maxX);
        mouseWorldPosition.y = Mathf.Clamp(mouseWorldPosition.y, minY, maxY);

        transform.position = mouseWorldPosition;


        // Ẩn con trỏ chuột
        Cursor.visible = false;

        // Khóa con trỏ chuột trong game
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Attack()
    {
        //Input.GetMouseButtonDown(0) &&
        if (Time.time > attackTime + attackCooldown)
        {
            Instantiate(bulletPrefab[powerLv], firePoint.transform.position, Quaternion.identity);
            attackTime = Time.time;
            if (powerLv == 1)
            {
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, -30));
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, 30));
            }

            if (powerLv == 2)
            {
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, -30));
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, 30));
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, -60));
                Instantiate(bulletPrefab[powerLv - 1], firePoint.transform.position, Quaternion.Euler(0, 0, 60));
            }
        }
    }


    IEnumerator TakeDame(float dame)
    {
        if (!isImmute)
        {
            hp -= dame;
            if(powerLv > 0) powerLv--;
            animator.SetTrigger("Hurt");
            if (hp <= 0)
            {
                animator.SetBool("Dead", true);
                Destroy(this.gameObject, 1f);
            }
            else
            {                
                isImmute = true;
                yield return new WaitForSeconds(2f);
                isImmute = false;
            }
        }
    }

    void UpdateHp()
    {
        for (int i = 0; i < 3; i++)
        {
            hearts[i].enabled = i < hp;
        }
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Present"))
            {
                if (powerLv < 2) powerLv++;
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Egg"))
            {
                StartCoroutine(TakeDame(1));
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Leg"))
            {
                score++;                
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(TakeDame(1));
                collision.gameObject.GetComponent<CS_EnemyControl>().TakeDame(1);
            }
            if (collision.gameObject.CompareTag("EggBoss"))
            {
                StartCoroutine(TakeDame(2));                
            }
            if (collision.gameObject.CompareTag("Boss"))
            {
                StartCoroutine(TakeDame(2));
            }
        }
    }

}
