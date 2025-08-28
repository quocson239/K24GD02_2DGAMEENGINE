using UnityEngine;

public class CS_BulletController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * 5f;
        if(transform.position.y >= 6)
        {
            Destroy(this.gameObject);
        }
    }
}
