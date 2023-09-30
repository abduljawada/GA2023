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
    private float _moveDir;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float minJumpTime = 0.2f;
    [SerializeField] private float velocityFallMultiplier = 0.5f;
    private float _timeSinceJump;
    
    [Header("Attack")]
    [SerializeField] private GameObject hitbox;
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
        
        if(isGrounded)
        {
            if (_moveDir != 0f)
            {
                OnWalk?.Invoke(this, new EntityEventArgs(){Dir = _moveDir});
            }
            else
            {
                OnStop?.Invoke(this, new EntityEventArgs(){Dir = _moveDir});
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        }
        else
        {
            OnJump?.Invoke(this, new EntityEventArgs(){Dir = _moveDir});
        }
        
        _timeSinceJump += Time.deltaTime;

        //TODO: Add Space for jumping
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

        if (!Input.GetKeyDown(KeyCode.Z) || !(Time.time > _nextAttackTime)) return;
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
        hitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitbox.SetActive(false);
        _isAttacking = false;
    }
}
