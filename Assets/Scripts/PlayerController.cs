using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class PlayerController : MonoBehaviour
{
    public event EventHandler<EntityEventArgs> OnStop;
    public event EventHandler<EntityEventArgs> OnWalk;
    public event EventHandler<EntityEventArgs> OnJump;
    
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    private GroundChecker GroundChecker => GetComponent<GroundChecker>();

    [SerializeField] private SaveData saveData;
    
    [Header("Speed")]
    [SerializeField] private float acceleration = 7.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float maxFallSpeed = -20f;
    private float _moveDir;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float minJumpTime = 0.2f;
    [SerializeField] private float velocityFallMultiplier = 0.5f;
    [SerializeField] private float jumpPressRememberTime = 0.2f;
    [SerializeField] private float groundedRememberTime = 0.2f;
    private float _timeSinceGrounded;
    private float _timeSinceJumpPress;
    private float _timeSinceJump;
    
    [Header("Attack")]
    [SerializeField] private GameObject hitBox;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackTime = 0.3f;
    private float _nextAttackTime;
    private bool _isAttacking;

    private void Start()
    {
        transform.position = saveData.spawnPos;
    }

    private void Update()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");

        var isGrounded = GroundChecker.IsGrounded();
        
        _timeSinceJumpPress -= Time.deltaTime;
        _timeSinceGrounded -= Time.deltaTime;

        if (_timeSinceJumpPress > 0 && _timeSinceGrounded > 0)
        {
            Jump();
            _timeSinceJumpPress = 0;
            _timeSinceGrounded = 0;
        }
        
        if(isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                _timeSinceJumpPress = jumpPressRememberTime;
            
            _timeSinceGrounded = groundedRememberTime;
            
            if (_moveDir != 0f)
            {
                OnWalk?.Invoke(this, new EntityEventArgs {Dir = _moveDir});
            }
            else
            {
                OnStop?.Invoke(this, new EntityEventArgs {Dir = _moveDir});
            }
        }
        else
        {
            OnJump?.Invoke(this, new EntityEventArgs {Dir = _moveDir});
            if (Rigidbody2D.velocity.y < maxFallSpeed)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxFallSpeed);
            }
        }
        
        _timeSinceJump += Time.deltaTime;
        
        if (Rigidbody2D.velocity.y > 0 && _timeSinceJump >= minJumpTime && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W)) 
        {
            Rigidbody2D.velocity *= new Vector2(1, velocityFallMultiplier);
        }

        if (!_isAttacking)
        {
            hitBox.transform.localPosition = _moveDir switch
            {
                > 0 => new Vector3(1.0f, 0),
                < 0 => new Vector3(-1.0f, 0),
                _ => hitBox.transform.localPosition
            };
        }

        if ((!Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.RightShift)) || !(Time.time > _nextAttackTime)) return;
        _nextAttackTime = Time.time + attackRate;
        StartCoroutine(AttackCoroutine());

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
        hitBox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitBox.SetActive(false);
        _isAttacking = false;
    }
}
