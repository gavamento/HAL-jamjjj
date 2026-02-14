using UnityEngine;

/// <summary>
/// WaveのX位置に合わせてカメラのX軸だけを追従させる
/// Wave_Move.cs の Wave_X_Pos を参照する
/// </summary>
public class Camera_FollowWave : MonoBehaviour
{
    [Header("X軸オフセット")]
    [Tooltip("WaveのXに対するカメラのオフセット（例: 0でWaveと同位置、負で左にずらす）")]
    public float offsetX = 0f;

    [Header("スムージング（任意）")]
    [Tooltip("0で即追従、大きいほどゆっくり追従")]
    public float positionSmoothTime = 0.15f;

    float _velocityX;

    void LateUpdate()
    {
        float targetX = Wave_Move.Wave_X_Pos + offsetX;
        Vector3 pos = transform.position;

        if (positionSmoothTime <= 0f)
        {
            pos.x = targetX;
        }
        else
        {
            pos.x = Mathf.SmoothDamp(pos.x, targetX, ref _velocityX, positionSmoothTime);
        }

        transform.position = pos;
    }
}
