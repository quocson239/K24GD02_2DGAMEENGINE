using UnityEngine;

public class CS_Present : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position += Vector3.down * 1.5f * Time.deltaTime; 
    }
}
