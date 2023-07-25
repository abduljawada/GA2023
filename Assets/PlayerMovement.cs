using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

    private float _moveDir;
    
    [Header("Speed")]
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float maxSpeed = 5f;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float minJumpTime = 0.2f;
    [SerializeField] private float velocityFallMultiplier = 0.5f;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius = 0.4f;
    
    private float _timeSinceJump;

    private void Update()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded()) Jump();
        
        _timeSinceJump += Time.deltaTime;

        if (Rigidbody2D.velocity.y > 0 && _timeSinceJump >= minJumpTime && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) 
        {
            Rigidbody2D.velocity *= new Vector2(1, velocityFallMultiplier);
        }
    }

    private void FixedUpdate()
    {
        var moveVelocityX = Mathf.Lerp(Rigidbody2D.velocity.x, _moveDir * maxSpeed, Time.deltaTime * acceleration);

        Rigidbody2D.velocity = new Vector2(moveVelocityX, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.velocity *= Vector2.right;
        Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _timeSinceJump = 0;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position - Vector3.up * 0.5f, circleRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - Vector3.up * 0.5f, circleRadius);
    }
}
