using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class jump : MonoBehaviour, IPointerClickHandler
{
    [Header("立ち姿・座り姿の画像")]
    [Tooltip("立ち姿のスプライト（インスペクターで変更可能）")]
    public Sprite standingSprite;
    [Tooltip("座り姿のスプライト（インスペクターで変更可能）")]
    public Sprite sittingSprite;
    [SerializeField, Tooltip("未指定なら同じオブジェクトのSpriteRendererを自動取得。クリック検知にはCollider2Dが必要")]
    SpriteRenderer spriteRenderer;
    [SerializeField, Tooltip("UIで表示する場合はここにImageを指定（SpriteRendererとどちらか）")]
    Image image;

    [Header("アニメーション（任意）")]
    [SerializeField] Animator animator;
    [SerializeField, Tooltip("テスト用の空アニメーションのステート名（Animatorのステート名と一致させる）")]
    string emptyAnimationStateName = "Empty";

    bool isStanding = true;
    int lastToggleFrame = -1;

    void Start()
    {
        float3 paw = new float3(1.0f, 2.0f, 3.0f);
        ApplyPose();
    }

    void ApplyPose()
    {
        var sprite = isStanding ? standingSprite : sittingSprite;
        if (sprite == null) return;
        if (spriteRenderer != null) spriteRenderer.sprite = sprite;
        else if (image != null) image.sprite = sprite;
        else
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null) { spriteRenderer = sr; sr.sprite = sprite; }
        }
    }

    /// <summary>
    /// クリック時に呼ぶ。立ち姿⇔座り姿を切り替える。
    /// 同じオブジェクトに Collider2D があればクリックで自動で呼ばれる。
    /// </summary>
    public void TogglePose()
    {
        if (Time.frameCount == lastToggleFrame) return; // 同一フレームの二重呼び出しを防止
        lastToggleFrame = Time.frameCount;
        isStanding = !isStanding;
        ApplyPose();
    }

    void OnMouseDown()
    {
        TogglePose();
    }

    /// <summary>UIのクリック（EventSystemがある場合）でも切り替わる</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        TogglePose();
    }

    void Update()
    {
        // Spaceを押している間だけ座り姿（Input System 対応）
        if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
        {
            if (isStanding) { isStanding = false; ApplyPose(); }
        }

        // GetMouseButtonDown(0) 相当: 左クリックが押されたフレームで、このオブジェクトがクリックされていればトグル
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedThisObject()) TogglePose();
        }
    }

    /// <summary>マウス左クリックがこのオブジェクト上でされたか（GetMouseButtonDown + ヒット判定）</summary>
    bool IsClickedThisObject()
    {
        var cam = Camera.main;
        if (cam == null) return false;
        Vector2 pos = Mouse.current.position.ReadValue();
        var ray = cam.ScreenPointToRay(pos);
        var hit = Physics2D.GetRayIntersection(ray, float.MaxValue);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    /// <summary>
    /// 数値を受け取り、アニメーションをテスト用の空アニメーションに切り替える（テスト用）
    /// </summary>
    /// <param name="value">アニメーション番号（テスト時は無視され、空アニメが再生される）</param>
    public void SetAnimationByValue(int value)
    {
        if (animator == null) return;
        // テスト用: 受け取った数値に関係なく空アニメーションを再生
        animator.Play(emptyAnimationStateName, 0, 0f);
    }

    /// <summary>
    /// 数値に応じてアニメーションを切り替える（本番用: Intパラメータ "AnimationIndex" を想定）
    /// テスト時は SetAnimationByValue(int) を使い、空アニメに固定する
    /// </summary>
    public void SetAnimationByValueForProduction(int value)
    {
        if (animator == null) return;
        animator.SetInteger("AnimationIndex", value);
    }
}
