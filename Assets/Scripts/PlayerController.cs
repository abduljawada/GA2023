using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();


    [Header("Speed")]
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float maxSpeed = 5f;
    private float _moveDir;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float minJumpTime = 0.2f;
    [SerializeField] private float velocityFallMultiplier = 0.5f;
    private float _timeSinceJump;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius = 0.4f;
    
    [Header("Attack")]
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackTime = 0.3f;
    private float _nextAttackTime;
    private bool _isAttacking;

    

    private void Update()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) Jump();
        
        _timeSinceJump += Time.deltaTime;

        if (Rigidbody2D.velocity.y > 0 && _timeSinceJump >= minJumpTime && !Input.GetKey(KeyCode.UpArrow)) 
        {
            Rigidbody2D.velocity *= new Vector2(1, velocityFallMultiplier);
        }

        if (!_isAttacking)
        {
            hitbox.transform.localPosition = _moveDir switch
            {
                > 0 => new Vector3(1.0f, 0),
                < 0 => new Vector3(-1.0f, 0),
                _ => hitbox.transform.localPosition
            };
        }

        if (Input.GetKeyDown(KeyCode.Z) && Time.time > _nextAttackTime)
        {
            _nextAttackTime = Time.time + attackRate;
            StartCoroutine(AttackCoroutine());
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
    
    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        hitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitbox.SetActive(false);
        _isAttacking = false;
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
