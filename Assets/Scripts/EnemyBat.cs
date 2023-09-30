using System;
using UnityEngine;

public class EnemyBat : Enemy
{
    [Header("Specific Attributes")]
    [SerializeField] private float speed = 3f;
    private Vector2 _attackPos;
    private Vector2 _originalPos;
    private bool _reachedAttackPos;
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
                CheckPlayerInRange();
                break;
            case States.Chase:
                if (Rigidbody2D.velocity.Equals(Vector2.zero))
                {
                    _reachedAttackPos = false;
                    TransitionToIdle();
                }

                if (_reachedAttackPos)
                {
                    if (Rigidbody2D.velocity.y != 0)
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
                }
                else
                {
                    var dir = Rigidbody2D.velocity.x;
                    if (dir != 0)
                    {
                        OnChangeDir(new EntityEventArgs(){Dir = dir});
                    }
                    if (Rigidbody2D.velocity.y != 0f)
                    {
                        if (Rigidbody2D.velocity.y < 0f && transform.position.y <= _attackPos.y ||
                            Rigidbody2D.velocity.y > 0f && transform.position.y >= _attackPos.y)
                        {
                            _reachedAttackPos = true;
                            Rigidbody2D.velocity = (_originalPos - (Vector2)transform.position).normalized * speed;
                        }
                    }
                    else
                    {
                        if (Rigidbody2D.velocity.x < 0f && transform.position.x <= _attackPos.x ||
                            Rigidbody2D.velocity.x > 0f && transform.position.x >= _attackPos.x)
                        {
                            _reachedAttackPos = true;
                            Rigidbody2D.velocity = (_originalPos - (Vector2)transform.position).normalized * speed;
                        }
                    }
                }

                break;
                default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnHitPlayer()
    {
        if (State != States.Chase) return;
        _reachedAttackPos = true;
        Rigidbody2D.velocity = (_originalPos - (Vector2)transform.position).normalized * speed;
    }

    protected override void TransitionToChase(Collider2D player)
    {
        base.TransitionToChase(player);
        
        _attackPos = player.transform.position;
        Rigidbody2D.velocity = (_attackPos - (Vector2)transform.position).normalized * speed;
    }
}
