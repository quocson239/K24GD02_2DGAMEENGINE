using UnityEngine;

public class CS_LegChicken : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * 2.5f;
        if(transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }
}
