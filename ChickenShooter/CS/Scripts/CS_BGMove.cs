using UnityEngine;

public class CS_BGMove : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
        if(transform.position.y <= -10) transform.position = new Vector3(transform.position.x, 12, transform.position.z);
    }
}
