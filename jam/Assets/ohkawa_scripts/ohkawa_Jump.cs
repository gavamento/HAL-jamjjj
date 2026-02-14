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
    }

    // Update is called once per frame
    void Update()
    {
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
                    JumpPower++;
                    Debug.Log("Jumpower: " + JumpPower);
                }
            }
        }

        //if内の条件でシーン遷移(落ちるシーンに以降)
        if (JumpPower >= 10)
        {
            SceneManager.LoadScene(fallSceneName);
        }
    }
}