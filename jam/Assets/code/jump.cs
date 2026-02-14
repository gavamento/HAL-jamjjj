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
    [SerializeField, Tooltip("未指定なら同じオブジェクトのSpriteRendererを自動取得。クリック検知には3DのColliderが必要")]
    SpriteRenderer spriteRenderer;

    [Header("アニメーション（任意）")]
    [SerializeField] Animator animator;
    [SerializeField, Tooltip("テスト用の空アニメーションのステート名（Animatorのステート名と一致させる）")]
    string emptyAnimationStateName = "Empty";

    bool isStanding = true;
    int lastToggleFrame = -1;

    void Start()
    {
        float3 paw = new float3(1.0f, 2.0f, 3.0f);
        ResolveDisplayComponent();
        ApplyPose();
    }

    /// <summary>表示先の SpriteRenderer または Image を確定（未代入なら自分/子から取得）</summary>
    void ResolveDisplayComponent()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>() ?? GetComponentInChildren<SpriteRenderer>();
    }

    void ApplyPose()
    {
        var sprite = isStanding ? standingSprite : sittingSprite;
        if (sprite == null) return;

        if (spriteRenderer == null) ResolveDisplayComponent();

        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;
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
        // Spaceを押している間は座り姿、離したら立ち姿に戻す（Input System 対応）
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                if (isStanding) { isStanding = false; ApplyPose(); }
            }
            else
            {
                if (!isStanding) { isStanding = true; ApplyPose(); }
            }
        }

        // 新 Input System: 左クリックが押されたフレームで、このオブジェクトがクリックされていればトグル
        if (WasLeftClickPressedThisFrame() && IsClickedThisObject())
            TogglePose();
    }

    /// <summary>新 Input System で左クリックが「このフレームで押された」か</summary>
    static bool WasLeftClickPressedThisFrame()
    {
        if (Mouse.current == null) return false;
        return Mouse.current.leftButton.wasPressedThisFrame;
    }

    /// <summary>新 Input System のマウス位置で、このオブジェクトがクリックされたか（3D Raycast で判定）</summary>
    bool IsClickedThisObject()
    {
        var cam = Camera.main;
        if (cam == null || Mouse.current == null) return false;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0f));

        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) return false;
        return hit.collider.gameObject == gameObject || hit.collider.transform.IsChildOf(transform);
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
