using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// JumpPowerが増えるたびにゲージを増やして表示する
/// JumpPowerScript.JumpPower (0～maxPower) に連動
/// </summary>
public class JumpPowerGauge : MonoBehaviour
{
    [Header("ゲージUI")]
    [Tooltip("ゲージの増減を表示するImage（Image Type = Filled にすること）")]
    public Image gaugeImage;

    [Header("設定")]
    [Tooltip("ゲージが満タンになるJumpPowerの値（JumpPowerScript側の閾値と合わせる）")]
    public int maxPower = 10;

    [Tooltip("ゲージの変化を滑らかにする（0で即反映）")]
    public float smoothSpeed = 8f;

    float _currentFill;

    void Start()
    {
        if (gaugeImage != null)
        {
            gaugeImage.type = Image.Type.Filled;
            gaugeImage.fillMethod = Image.FillMethod.Horizontal;
            gaugeImage.fillOrigin = (int)Image.OriginHorizontal.Left;  // 左端から増える
            _currentFill = JumpPowerScript.JumpPower / (float)maxPower;
            gaugeImage.fillAmount = _currentFill;
        }
    }

    void Update()
    {
        if (gaugeImage == null) return;

        float targetFill = Mathf.Clamp01(JumpPowerScript.JumpPower / (float)maxPower);

        if (smoothSpeed <= 0f)
        {
            _currentFill = targetFill;
        }
        else
        {
            _currentFill = Mathf.MoveTowards(_currentFill, targetFill, smoothSpeed * Time.deltaTime);
        }

        gaugeImage.fillAmount = _currentFill;
    }
}
