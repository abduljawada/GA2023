using UnityEngine;

public class FlyingEnemy : Enemy
{
    [Header("Specific Attributes")]
    [SerializeField] private Vector2 flyPoint;
    [SerializeField] private float flyDistance = 3f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float attackRate = 5f;
    [SerializeField] private Vector2 eggOffset;
    private float _timeToNextAttack;
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    private Animator animator;
    private void Start()
    {
        _timeToNextAttack = attackRate;
    }

    private void Update()
    {
        switch (State)
        {
            case States.Idle:
                CheckPlayerInRange();
                break;
            case States.Chase:
                if (Rigidbody2D.velocity.y != 0f)
                {
                    if (transform.position.y >= flyPoint.y)
                    {
                        Rigidbody2D.velocity = Vector2.right * speed;
                    }
                    
                    return;
                }
                
                if (transform.position.x > flyPoint.x + flyDistance)
                {
                    Rigidbody2D.velocity = Vector2.left * speed;
                }
                else if (transform.position.x < flyPoint.x - flyDistance)
                {
                    Rigidbody2D.velocity = Vector2.right * speed;
                }

                _timeToNextAttack -= Time.deltaTime;

                if (_timeToNextAttack <= 0)
                {
                    _timeToNextAttack = attackRate;
                    Instantiate(eggPrefab, transform.position + (Vector3) eggOffset, Quaternion.identity);
                }
                break;
        }
    }

    protected override void TransitionToChase()
    {
        base.TransitionToChase();

        Vector2 flyingDir = ((Vector3)flyPoint - transform.position).normalized;
        Rigidbody2D.velocity = flyingDir * speed;
        

    }
}
