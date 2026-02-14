using Unity.Mathematics;
using UnityEngine;

public class jump : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField, Tooltip("テスト用の空アニメーションのステート名（Animatorのステート名と一致させる）")]
    string emptyAnimationStateName = "Empty";

    void Start()
    {
        float3 paw = new float3(1.0f, 2.0f, 3.0f);
    }

    void Update()
    {
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
