using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// JumpPowerが増えるたびにゲージを増やして表示する
/// 「Power Source」にJumpPowerScriptを指定すると確実に連動する
/// </summary>
public class JumpPowerGauge : MonoBehaviour
{
    [Header("参照（必須）")]
    [Tooltip("JumpPowerを管理しているJumpPowerScript。ここにアタッチ先を指定するとゲージが確実に連動します")]
    public JumpPowerScript powerSource;

    [Header("ゲージUI")]
    [Tooltip("ゲージの増減を表示するImage（Image Type = Filled）。未設定なら自分か子のImageを取得")]
    public Image gaugeImage;

    [Header("設定")]
    [Tooltip("ゲージが満タンになるJumpPowerの値")]
    public int maxPower = 600;

    [Tooltip("ゲージの変化を滑らかにする（0で即反映。増減が速い場合は0推奨）")]
    public float smoothSpeed = 0f;

    float _currentFill;

    void Start()
    {
        if (powerSource == null)
            Debug.LogWarning("JumpPowerGauge: 「Power Source」が未設定です。JumpPowerScriptが付いているオブジェクトを指定するとゲージが連動します。", this);
        if (gaugeImage == null)
            gaugeImage = GetComponent<Image>() ?? GetComponentInChildren<Image>();
        if (gaugeImage == null)
        {
            Debug.LogWarning("JumpPowerGauge: Gauge Image が未設定です。Inspectorで「Gauge Image」にゲージ用のImageを指定してください。", this);
            return;
        }
        gaugeImage.type = Image.Type.Filled;
        gaugeImage.fillMethod = Image.FillMethod.Horizontal;
        gaugeImage.fillOrigin = (int)Image.OriginHorizontal.Left;  // 左端から増える
        int power = GetPower();
        power = Mathf.Clamp(power, 0, maxPower);
        _currentFill = power / (float)maxPower;
        gaugeImage.fillAmount = _currentFill;
    }

    void LateUpdate()
    {
        if (gaugeImage == null) return;

        int power = GetPower();
        power = Mathf.Clamp(power, 0, maxPower);
        float targetFill = power / (float)maxPower;

        if (smoothSpeed <= 0f)
            _currentFill = targetFill;
        else
            _currentFill = Mathf.MoveTowards(_currentFill, targetFill, smoothSpeed * Time.deltaTime);

        gaugeImage.fillAmount = _currentFill;
    }

    int GetPower()
    {
        if (powerSource != null)
            return powerSource.GetCurrentPower();
        return JumpPowerScript.JumpPower;
    }
}
