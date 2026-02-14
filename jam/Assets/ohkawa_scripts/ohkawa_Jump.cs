using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpPowerScript : MonoBehaviour
{
    public string fallSceneName = "SampleTitle";    //ジャンプのシーンが終わったらこの名前のシーンに遷移する
    public KeyCode clickKey = KeyCode.Mouse0;       //ジャンプするためのキー

    GameObject clickedGameObject;//クリックされたゲームオブジェクトを代入する変数

    public static int JumpPower = 0;                //ジャンプするためのパワー(仮)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        JumpPower = 0;      //初期化処理。一旦パワーだけ0にしています。
        //JumpPowerはゲージのMAX値が600、一秒間に60(10%)減って、クリックで30(5%)増えるように設定しています。
    }

    // Update is called once per frame
    void Update()
    {
        if(JumpPower <= 0)
        {
            JumpPower = 1;
        }

        if (Input.GetKeyDown(clickKey))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                Debug.Log(clickedGameObject.name);//ゲームオブジェクトの名前を出力

                if(clickedGameObject.tag == "Player")
                {
                    //クリックでクリック回数を増加
                    JumpPower += 100;
                }
            }
        }

        JumpPower--;

        Debug.Log("Jumpower: " + JumpPower);

        //if内の条件でシーン遷移(落ちるシーンに以降)
        if (JumpPower >= 10)
        {
            //SceneManager.LoadScene(fallSceneName);
        }
    }
}