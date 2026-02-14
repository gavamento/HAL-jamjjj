using UnityEngine;

public class ohkawa_playerScript : MonoBehaviour
{
    enum PlayerState
    {
        AURA_STATE1 = 1,
        AURA_STATE2,
        AURA_STATE3,
        AURA_STATE4,
        AURA_STATE5
    }

    public struct Aurabool
    {
        public bool isAura;
        public bool isAuring;
    }


    public GameObject [] AuraArray = new GameObject[5];
    GameObject[] AuraObjectArray = new GameObject[5];
    Aurabool [] isAura = new Aurabool[5];

    PlayerState playerState = PlayerState.AURA_STATE1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            AuraObjectArray[i] = AuraArray[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (JumpPowerScript.JumpPower <= 120)
        {
            //if (isAura[0].isAura && isAura[0].isAuring)
            //{
            //    isAura[0].isAuring = false;
            //    //プレイヤーの状態をAURA_STATE1にする
            //    playerState = PlayerState.AURA_STATE1;
            //    Instantiate(AuraObjectArray[0], transform.position, Quaternion.identity);
            //}

            //プレイヤーの状態をAURA_STATE5にする
            playerState = PlayerState.AURA_STATE1;
            Destroy(AuraObjectArray[0]);
            Instantiate(AuraObjectArray[0], new Vector3(0,0.1f,2), Quaternion.identity);
        }
        else if (JumpPowerScript.JumpPower <= 240)
        {
            //if (isAura[1].isAura && isAura[1].isAuring)
            //{
            //    isAura[1].isAuring = false;
            //    //プレイヤーの状態をAURA_STATE2にする
            //    playerState = PlayerState.AURA_STATE2;
            //    Instantiate(AuraObjectArray[1], transform.position, Quaternion.identity);
            //}

            //プレイヤーの状態をAURA_STATE2にする
            playerState = PlayerState.AURA_STATE2;
            Destroy(AuraObjectArray[0]);
            Instantiate(AuraObjectArray[1], new Vector3(0, 0.2f, 1), Quaternion.identity);
        }
        else if (JumpPowerScript.JumpPower <= 360)
        {
            //if (isAura[2].isAura && isAura[2].isAuring)
            //{
            //    isAura[2].isAuring = false;
            //    //プレイヤーの状態をAURA_STATE3にする
            //    playerState = PlayerState.AURA_STATE3;
            //    Instantiate(AuraObjectArray[2], transform.position, Quaternion.identity);
            //}

            //プレイヤーの状態をAURA_STATE3にする
            playerState = PlayerState.AURA_STATE3;
            Destroy(AuraObjectArray[1]);
            Instantiate(AuraObjectArray[2], new Vector3(0, 0.3f, 0), Quaternion.identity);
        }
        else if (JumpPowerScript.JumpPower <= 480)
        {
            //if (isAura[3].isAura && isAura[3].isAuring)
            //{
            //    isAura[3].isAuring = false;
            //    //プレイヤーの状態をAURA_STATE4にする
            //    playerState = PlayerState.AURA_STATE4;
            //    Instantiate(AuraObjectArray[3], transform.position, Quaternion.identity);
            //}

            //プレイヤーの状態をAURA_STATE4にする
            playerState = PlayerState.AURA_STATE4;
            Destroy(AuraObjectArray[2]);
            Instantiate(AuraObjectArray[3], new Vector3(0, 0.4f, -1), Quaternion.identity);
        }
        else if (JumpPowerScript.JumpPower <= 600)
        {
            //if (isAura[4].isAura && isAura[4].isAuring)
            //{
            //    isAura[4].isAuring = false;
            //    //プレイヤーの状態をAURA_STATE5にする
            //    playerState = PlayerState.AURA_STATE5;
            //    Instantiate(AuraObjectArray[4], transform.position, Quaternion.identity);
            //}

            //プレイヤーの状態をAURA_STATE5にする
            playerState = PlayerState.AURA_STATE5;
            Destroy(AuraObjectArray[3]);
            Instantiate(AuraObjectArray[4], new Vector3(0, 0.5f, -2), Quaternion.identity);
        }
    }
}
