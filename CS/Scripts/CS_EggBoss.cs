using System.Collections;
using UnityEngine;

public class CS_EggBoss : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    Animator animator;
    bool isShot;
    bool is1;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0)
        {
            transform.position += Vector3.down * 2f * Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("Break");
            Destroy(this.gameObject, 0.5f);
            if(!is1) isShot = true;
            
        }
        if (isShot) 
        {
            StartCoroutine(shot());
            is1 = true;
        }

    }

    IEnumerator shot()
    {
        isShot = false;
        yield return new WaitForSeconds(0.3f);
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0, 0, 45));
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0, 0, 60));
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0, 0, -45));
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0, 0, -60));
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ship"))
        {
            animator.SetTrigger("Break");
            Destroy(this.gameObject, 0.5f);
            if (!is1) isShot = true;
        }
    }
}
