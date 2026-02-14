using Unity.Mathematics;
using UnityEngine;

public class Wave_Move : MonoBehaviour
{
    Rigidbody rb;

    Vector3 speed;

    //後でエネルギーを入れる
    float energy;

    float timer;

    public static float Wave_X_Pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0.0f;
        energy = 10;
        
        speed = new Vector3(0.0f, 0.0f, 0.0f);
        rb = this.GetComponent<Rigidbody>();
        Wave_X_Pos = rb.position.x;

    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            energy -= 1f;
            timer = 0f;
        }


        if (energy > 0)
        {
            //エネルギー量に応じた減速
            if (energy > 10)
            { speed = new Vector3(4.0f, 0.0f, 0.0f); }
            else if (energy > 5)
            { speed = new Vector3(3.0f, 0.0f, 0.0f); }
            else if (energy > 3)
            { speed = new Vector3(2.0f, 0.0f, 0.0f); }
            else
            { speed = new Vector3(1.0f, 0.0f, 0.0f); }
        }
        else
        {
            //エネルギー０で停止
            speed = new Vector3(0.0f, 0.0f, 0.0f);
        }

        Wave_X_Pos = rb.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = speed;
    }


}
