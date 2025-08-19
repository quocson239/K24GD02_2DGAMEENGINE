using UnityEngine;

public class Platformbay : MonoBehaviour
{
    Vector3 direct;
    Vector3 sdirect;
    Vector3 current;
    public bool right;
    public bool special;
    public float speed;
    void Start()
    {
        if (right) direct = Vector3.right;
        else direct = Vector3.left;
        current = transform.position;
        sdirect = Vector2.one;
    }

    // Update is called once per frame
    void Update()
    {
        if(special) {SpecialMove(); }
        else NormalMove();


    }

    void NormalMove()
    {
        transform.position += direct * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, current + 3 * direct) < 0.1f)
        {
            direct = (direct == Vector3.right) ? Vector3.left : Vector3.right;
        }
    }

    void SpecialMove()
    {
        transform.position += sdirect * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, current + 3 * sdirect) < 0.1f)
        {
            sdirect = (sdirect == (Vector3)Vector2.one) ? -Vector2.one : Vector2.one;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
