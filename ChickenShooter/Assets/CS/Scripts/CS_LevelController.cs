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
        StartCoroutine(Run());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("i = "+ i);
        if (num == 6)
        {
            CancelInvoke();
            num = 0;
            i = 0;
        }
    }

    void SpawnChicken(Vector3 pos)
    {
        float j = cSpawn == spawnPosA ? -1f : 1f;
        GameObject ck = Instantiate(chickenPrefab, cSpawn.position, Quaternion.Euler(0,0,j * 45));
        ck.GetComponent<CS_EnemyControl>().t = pos;
        ck.GetComponent<CS_EnemyControl>().isIntro = true;
        if(lv > 1) ck.GetComponent<CS_EnemyControl>().hp += 3;
        cSpawn = i < 2 ? spawnPosA : spawnPosB;
        num ++; 

    }

    void SpawnChickenVip()
    {
        float j = cSpawn == spawnPosA ? -1f : 1f;
        GameObject ck = Instantiate(chickenPrefab, cSpawn.position, Quaternion.Euler(0, 0, j * 45));        
        ck.GetComponent<CS_EnemyControl>().isCircle = true;
        ck.GetComponent<CS_EnemyControl>().isCMDone = true;
        if (lv > 1) ck.GetComponent<CS_EnemyControl>().hp += 10;                

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
        ckPos[0] = new Vector2(-5,3);
        ckPos[1] = new Vector2(-7, 1);
        ckPos[2] = new Vector2(-5, 1);
        ckPos[3] = new Vector2(5, 3);
        ckPos[4] = new Vector2(-7, 1);
        ckPos[5] = new Vector2(5, 1);

    }

    IEnumerator Run()
    {
        
        yield return new WaitForSeconds(15f);
        DestroyLeft();
        StartCoroutine(Level2());
        yield return StartCoroutine(Level2());
        DestroyLeft();
        StartCoroutine(Level3());
        yield return StartCoroutine(Level3());
        DestroyLeft();
        StartCoroutine(Level4());

    }

    IEnumerator Level2()
    {
        lv++;        
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
        yield return new WaitForSeconds(5f);

    }

    IEnumerator Level3()
    {
        lv++;        
        isLv3 = false;
        Lv3();
        yield return new WaitForSeconds(0.1f);
        InvokeRepeating("Lv1", 0, 0.5f);        
        InvokeRepeating("SpawnChickenVip", 0, 0.5f);
        yield return new WaitForSeconds(5f);        
        CancelInvoke();
        yield return new WaitForSeconds(5f);

    }

    IEnumerator Level4()
    {        
        isLv4 = false;
        Instantiate(bossPrefab, spawnPosC.position, Quaternion.identity);
        yield return null;
    }


    void DestroyLeft()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }    
}
