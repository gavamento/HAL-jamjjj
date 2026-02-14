using UnityEngine;

/// <summary>
/// 着地したときに衝撃波とWaveを出す。
/// ジャンプするRigidbodyが付いている同じオブジェクトにアタッチする。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class LandingShockwave : MonoBehaviour
{
    [Header("衝撃波")]
    [Tooltip("着地時に生成する衝撃波のプレハブ（毎回Instantiateする）")]
    public GameObject shockwavePrefab;

    [Tooltip("既存の衝撃波オブジェクトを指定すると、こちらを表示して使う（Prefabより優先）")]
    public GameObject shockwaveObject;

    [Tooltip("衝撃波を出す高さオフセット（足元に出す場合は0かマイナス）")]
    public float shockwaveHeightOffset = 0f;

    [Header("Wave生成")]
    [Tooltip("着地時に生成するWaveのプレハブ（Wave_Moveが付いたオブジェクトを指定）")]
    public GameObject wavePrefab;

    [Tooltip("Waveを出す高さオフセット（地面の高さに合わせて調整）")]
    public float waveHeightOffset = 0f;

    [Header("着地判定（任意）")]
    [Tooltip("指定すると、このレイヤーとの衝突時だけ衝撃波を出す。未指定なら全ての衝突で判定")]
    public LayerMask groundLayers = -1;

    [Tooltip("指定すると、このタグのオブジェクトとの衝突時だけ衝撃波を出す。未指定なら無視")]
    public string groundTag = "";

    bool _wasInAir;
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 上面からの着地か（足が地面に付いた）
        if (!IsGroundContact(collision)) return;

        if (_wasInAir)
        {
            SpawnShockwave(collision);
            SpawnWave(collision);
        }

        _wasInAir = false;
    }

    void OnCollisionExit(Collision collision)
    {
        if (IsGroundContact(collision))
            _wasInAir = true;
    }

    bool IsGroundContact(Collision collision)
    {
        if (groundLayers != -1 && ((1 << collision.gameObject.layer) & groundLayers) == 0)
            return false;
        if (!string.IsNullOrEmpty(groundTag) && !collision.gameObject.CompareTag(groundTag))
            return false;

        // 接触面の法線が上向きなら「地面」
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
                return true;
        }
        return false;
    }

    void SpawnShockwave(Collision collision)
    {
        Vector3 pos = transform.position + Vector3.up * shockwaveHeightOffset;
        if (collision.contactCount > 0)
            pos = collision.GetContact(0).point + Vector3.up * shockwaveHeightOffset;

        if (shockwaveObject != null)
        {
            shockwaveObject.transform.position = pos;
            shockwaveObject.SetActive(true);
        }
        else if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, pos, Quaternion.identity);
        }
    }

    void SpawnWave(Collision collision)
    {
        if (wavePrefab == null) return;

        Vector3 pos = transform.position + Vector3.up * waveHeightOffset;
        if (collision.contactCount > 0)
            pos = collision.GetContact(0).point + Vector3.up * waveHeightOffset;

        Instantiate(wavePrefab, pos, Quaternion.identity);
    }
}
