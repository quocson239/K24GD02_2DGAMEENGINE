using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUICanvas : MonoBehaviour
{
    public Image[] hearts;
    float hp;
    public GameObject GOpan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = GetComponent<PlayerController>().hp;
        UpdateHP();
        GO();
    }

    void UpdateHP()
    {
        for (int i = 0; i < 3; i++)
        {
            hearts[i].enabled = i < hp;
        }
    }

    void GO()
    {
        if (hp <= 0)
        {
            GOpan.SetActive(true);
        }
    }

    public void Again()
    {
        SceneManager.LoadScene("TestTileMap");
    }

}
