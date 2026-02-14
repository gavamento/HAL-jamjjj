using UnityEngine;

public class Enemy_Dead_Check : MonoBehaviour
{
    Rigidbody rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.x < Wave_Move.Wave_X_Pos)
        {
            Destroy(gameObject);
        }
    }
}
