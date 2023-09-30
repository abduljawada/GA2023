using System;
using UnityEngine;

public class AnimatorManagerEnemy : MonoBehaviour
{
    private static readonly int IsChasing = Animator.StringToHash("IsChasing");
    private Enemy Enemy => GetComponentInParent<Enemy>();
    private Animator Animator => GetComponent<Animator>();
    private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

    private void Start()
    {
        Enemy.OnIdle += EnemyOnOnIdle;
        Enemy.OnChase += EnemyOnOnChase;
        Enemy.ChangeDir += EnemyOnChangeDir;
    }

    private void EnemyOnOnIdle(object sender, EventArgs e)
    {
        Animator.SetBool(IsChasing, false);
    }

    private void EnemyOnOnChase(object sender, EventArgs e)
    {
        Animator.SetBool(IsChasing, true);
    }

    private void EnemyOnChangeDir(object sender, EntityEventArgs e)
    {
        SpriteRenderer.flipX = e.Dir > 0;
    }
}
