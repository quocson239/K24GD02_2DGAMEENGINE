using UnityEngine;
using UnityEngine.UI;

public class PlayerUICanvas : MonoBehaviour
{
    public Image[] hearts;
    float hp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = GetComponent<PlayerController>().hp;
        UpdateHP();
    }

    void UpdateHP()
    {
        for (int i = 0; i < 3; i++)
        {
            hearts[i].enabled = i < hp;
        }
    }

}
