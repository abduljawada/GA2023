using System;
using UnityEngine;

public class EnemyBat : Enemy
{
    [SerializeField] private float speed = 3f;
    private Vector2 _attackPos;
    private Vector2 _originalPos;
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

    private void Start()
    {
        _originalPos = transform.position;
    }

    private void Update()
    {
        switch (State)
        {
            case States.Idle:
                if (Rigidbody2D.velocity == Vector2.zero)
                {
                    CheckPlayerInRange();
                }
                else if (Rigidbody2D.velocity.y != 0)
                {
                    if (Rigidbody2D.velocity.y < 0f && transform.position.y <= _originalPos.y ||
                        Rigidbody2D.velocity.y > 0f && transform.position.y >= _originalPos.y)
                    {
                        Rigidbody2D.velocity = Vector2.zero;
                    }
                }
                else
                {
                    if (Rigidbody2D.velocity.x < 0f && transform.position.x <= _originalPos.x ||
                        Rigidbody2D.velocity.x > 0f && transform.position.x >= _originalPos.x)
                    {
                        Rigidbody2D.velocity = Vector2.zero;
                    }
                }
                break;
            case States.Chase:
                if (Rigidbody2D.velocity.y != 0f)
                {
                    if (Rigidbody2D.velocity.y < 0f && transform.position.y <= _attackPos.y ||
                        Rigidbody2D.velocity.y > 0f && transform.position.y >= _attackPos.y)
                    {
                        TransitionToIdle();
                    }
                }
                else
                {
                    if (Rigidbody2D.velocity.x < 0f && transform.position.x <= _attackPos.x ||
                        Rigidbody2D.velocity.x > 0f && transform.position.x >= _attackPos.x)
                    {
                        TransitionToIdle();
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnHitPlayer()
    {
        TransitionToIdle();
    }

    protected override void TransitionToIdle()
    {
        base.TransitionToIdle();
        
        Rigidbody2D.velocity = (_originalPos - (Vector2)transform.position).normalized * speed;
    }

    protected override void TransitionToChase(Collider2D player)
    {
        base.TransitionToChase(player);
        
        _attackPos = player.transform.position;
        Rigidbody2D.velocity = (_attackPos - (Vector2)transform.position).normalized * speed;
    }
}
