using UnityEngine;

public class EnemyJumping : Enemy
{
    [Header("Specific Attributes")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius = 0.4f;
    [SerializeField] private Vector2 jumpDir = new(-2f, 10f);

    [SerializeField] private float timeOnGround = 2f;
    private float _timeToNextJump;
    private readonly Vector2 _flipXVector2 = new Vector2(-1, 1);
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

    private void Start()
    {
        _timeToNextJump = timeOnGround;
    }

    private void Update()
    {
        if (!IsGrounded()) return;
        
        _timeToNextJump -= Time.deltaTime;

        if (!(_timeToNextJump <= 0)) return;
        _timeToNextJump = timeOnGround;
        Rigidbody2D.AddForce(jumpDir, ForceMode2D.Impulse);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position - Vector3.up * 0.5f, circleRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: Fix Collision
        if (!other.gameObject.tag.Equals("Obstacle") && !other.gameObject.tag.Equals("Player")) return;
        jumpDir *= _flipXVector2;
        Rigidbody2D.velocity = new Vector2(jumpDir.x, Rigidbody2D.velocity.y);
    }
}
