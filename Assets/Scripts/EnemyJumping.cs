using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class EnemyJumping : Enemy
{
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    private GroundChecker GroundChecker => GetComponent<GroundChecker>();
    
    [Header("Specific Attributes")] 
    [SerializeField] private Vector2 jumpDir = new(-2f, 10f);

    [SerializeField] private float timeOnGround = 2f;
    private float _timeToNextJump;
    private readonly Vector2 _flipXVector2 = new(-1, 1);
    private SpriteRenderer SpriteRenderer => GetComponentInChildren<SpriteRenderer>();

    private void Start()
    {
        _timeToNextJump = timeOnGround;
    }

    private void Update()
    {
        if (!GroundChecker.IsGrounded()) return;
        _timeToNextJump -= Time.deltaTime;
        if (!(_timeToNextJump <= 0)) return;
        _timeToNextJump = timeOnGround;
        Rigidbody2D.AddForce(jumpDir, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D()
    {
        if (GroundChecker.IsGrounded()) return;
        jumpDir *= _flipXVector2;
        SpriteRenderer.flipX = !SpriteRenderer.flipX;
        Rigidbody2D.velocity = new Vector2(jumpDir.x, Rigidbody2D.velocity.y);
    }
}
