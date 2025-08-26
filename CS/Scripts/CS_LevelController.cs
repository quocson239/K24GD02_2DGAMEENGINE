using System.Collections;
using UnityEngine;

public class CS_LevelController : MonoBehaviour
{
    int lv = 1;
    public int num;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject bossPrefab;
    Vector3[] ckPos = new Vector3[6];
    float x = -7;
    float y = 3;
    int i = 0;

    bool isLv2;
    bool isLv3;
    bool isLv4;
    [SerializeField] Transform spawnPosA;
    [SerializeField] Transform spawnPosB;
    [SerializeField] Transform spawnPosC;

    Transform cSpawn ;
    private void Awake()
    {
        cSpawn = spawnPosA;
        for (int i = 0; i < 6; i++)
        {
            ckPos[i] = new Vector2(x + i * (2.5f), y);
        }
    }
    void Start()
    {                
        InvokeRepeating("Lv1", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("i = "+ i);
        if (i == 6 && lv == 1 && num == 0)
        {
            CancelInvoke();
            lv++;
            i = 0;
            isLv2 = true;
            cSpawn = spawnPosA;
        }
        
        if (num == 0 && lv == 2 && isLv2)
        {
            StartCoroutine(Level2());
        }

        if (i == 6 && lv == 2 && num == 0)
        {
            CancelInvoke();
            lv++;
            i = 0;
            isLv3 = true;
            cSpawn = spawnPosA;            
        }

        if (num == 0 && lv == 3 && isLv3)
        {
            StartCoroutine(Level3());
        }

        if (i == 6 && lv == 3 && num < 0)
        {
            Debug.Log("?????????????????????????????");
            CancelInvoke();
            lv++;
            i = 0;
            isLv4 = true;
            cSpawn = spawnPosA;
            num = 0;
        }

        if (lv == 4 && isLv4)
        {
            StartCoroutine(Level4());   
        }
    }

    void SpawnChicken(Vector3 pos)
    {
        float j = cSpawn == spawnPosA ? -1f : 1f;
        GameObject ck = Instantiate(chickenPrefab, cSpawn.position, Quaternion.Euler(0,0,j * 45));
        ck.GetComponent<CS_EnemyControl>().t = pos;
        ck.GetComponent<CS_EnemyControl>().isIntro = true;
        if(lv > 1) ck.GetComponent<CS_EnemyControl>().hp += 1;
        cSpawn = i < 2 ? spawnPosA : spawnPosB;
        num ++; 

    }

    void SpawnChickenVip()
    {
        float j = cSpawn == spawnPosA ? -1f : 1f;
        GameObject ck = Instantiate(chickenPrefab, cSpawn.position, Quaternion.Euler(0, 0, j * 45));        
        ck.GetComponent<CS_EnemyControl>().isCircle = true;
        ck.GetComponent<CS_EnemyControl>().isCMDone = true;
        if (lv == 3) ck.GetComponent<CS_EnemyControl>().hp += 2;        
        num++;

    }

    void Lv1()
    {
        
        if (i < ckPos.Length)
        {
            SpawnChicken(ckPos[i]);
            i++;
            
        }    
            
    }
    void Lv2()
    {               
        for (int i = 0; i < 6; i++)
        {
            ckPos[i] = new Vector2(x + 1 + i * (2.5f), y - 1);
        }            
    }
    void Lv3()
    {
        for (int i = 0; i < 6; i++)
        {
            ckPos[i] = new Vector2(x + i * (3f), y - 2 * (i % 2));
        }
    }

    IEnumerator Level2()
    {
        isLv2 = false;
        InvokeRepeating("Lv1", 0, 0.5f);
        yield return new WaitForSeconds(3f);        
        CancelInvoke();
        i = 0;            
        yield return new WaitForSeconds(0.1f);
        Lv2();
        cSpawn = spawnPosA;
        yield return new WaitForSeconds(0.2f);
        InvokeRepeating("Lv1", 0f, 0.5f);
        yield return new WaitForSeconds(3.5f);
        CancelInvoke();
        
    }

    IEnumerator Level3()
    {
        isLv3 = false;
        Lv3();
        yield return new WaitForSeconds(0.1f);
        InvokeRepeating("Lv1", 0, 0.5f);
        InvokeRepeating("SpawnChickenVip", 0.5f, 0.5f);
        yield return new WaitForSeconds(5f);        
        CancelInvoke();
        i = 6;
        
    }

    IEnumerator Level4()
    {
        isLv4 = false;
        Instantiate(bossPrefab, spawnPosC.position, Quaternion.identity);
        yield return null;
    }

}
