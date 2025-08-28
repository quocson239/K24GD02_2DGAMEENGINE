using UnityEngine;

public class CS_EGroup : MonoBehaviour
{
    Vector3 direct;
    void Start()
    {
        direct = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direct * Time.deltaTime * 2f;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            direct = (direct == Vector3.right) ? Vector3.left : Vector3.right;
        }
    }
    


}
