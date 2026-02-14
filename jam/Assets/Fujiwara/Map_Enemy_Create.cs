using UnityEngine;

public class Map_Enemy_Create : MonoBehaviour
{

    int EnemyMax;
    int EnemyNum;

    public GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyMax = 100;
        EnemyNum = 0;

        for (int i = 0; i < EnemyMax; i++)
        {

            float ran = i + Random.Range(3.0f, 7.0f);

            Vector3 pos = transform.position + transform.forward * 1.5f;
            pos.x = Wave_Move.Wave_X_Pos + ran;
            pos.y += 1;

            GameObject obj = Instantiate(enemy, pos, Quaternion.identity);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (EnemyNum < EnemyMax)
        {
            float ran = EnemyNum + Random.Range(3.0f, 7.0f);
            Vector3 pos = transform.position + transform.forward * 1.5f;
            pos.x = Wave_Move.Wave_X_Pos + ran;
            pos.y += 1;
            GameObject obj = Instantiate(enemy, pos, Quaternion.identity);
            EnemyNum++;
        }
    }
}
