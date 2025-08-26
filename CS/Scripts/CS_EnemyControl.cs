using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CS_EnemyControl : MonoBehaviour
{
    public float hp = 3;

    [SerializeField] GameObject eggPrefabs;
    [SerializeField] GameObject eggPos;
    [SerializeField] GameObject legChicken;
    [SerializeField] GameObject presentPrefabs;
    GameObject Ship;
    GameObject lvController;
    float time;
    Animator animator;

    bool isActive;
    public bool isIntro;
    public bool isCircle;
    public Vector3 t;

    float radius = 5f; // Bán kính của quỹ đạo
    float speed = 2f; // Tốc độ di chuyển
    float angle; // Góc hiện tại

    public bool isCMDone;
    void Start()
    {
        animator = GetComponent<Animator>();
        time = Random.Range(5, 10);
        
        Ship = GameObject.Find("Ship");
        lvController = GameObject.Find("LvController");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntro) Intro(t);
        if (isCircle) CircleMove();
        if (isActive)
        {
            InvokeRepeating("SpawnEggs", time, time);
            isActive = false;
        }
    }

    public void Intro(Vector3 t)
    {
        
        if(Vector3.Distance(transform.position, t) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t, 10f * Time.deltaTime);
            
        }

        if(transform.rotation.z > 0)
        {
            transform.rotation *= Quaternion.Euler(0, 0, -1 * Time.deltaTime * 65f);
        }
        if (transform.rotation.z < 0)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 1 * Time.deltaTime * 65f);
        }
        
        if (Vector3.Distance(transform.position, t) <= 0.1f && Mathf.Abs(transform.rotation.eulerAngles.z)  < 0.2)
        {
            isActive = true;
            isIntro = false;
        }    
    }


    void CircleMove()
    {        
        // Tính toán góc mới theo thời gian
        angle += speed * Time.deltaTime;

        // Tính toán vị trí mới
        float x = Mathf.Cos(angle) * radius; // Tính toán tọa độ x
        float y = Mathf.Sin(angle) * radius; // Tính toán tọa độ y

        // Cập nhật vị trí của vật thể
        transform.position = new Vector3(x, y, angle);        
        
        if(isCMDone)
        {
            isActive = true ;
            isCMDone = false ;
        }
    }

    
    void SpawnEggs()
    {
        Instantiate(eggPrefabs, eggPos.transform.position, Quaternion.identity);
    }


    void DropPresent()
    {
        Instantiate(presentPrefabs, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDame(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDame(int dame)
    {
        hp -= dame;
        animator.SetTrigger("Hurt");
        int n = Random.Range(1, 5);
        if (hp <= 0)
        {
            Ship.GetComponent<CS_ShipController>().score += 5;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 0.5f);
            animator.SetBool("Dead", true);
            Instantiate(legChicken, transform.position, Quaternion.identity);
            if (n == 2) DropPresent();
            lvController.GetComponent<CS_LevelController>().num--;
        }
    }
}
