using UnityEngine;

public class EnemyBear : Enemy
{
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    [Header("Specific Attributes")]
    [SerializeField] private float speed = 3.5f;
    
    private void OnEnable()
    {
        Rigidbody2D.velocity = Vector2.right * speed;
        transform.localScale = Vector3.one;
    }

    private void OnCollisionEnter2D()
    {
        Rigidbody2D.velocity *= -1;
        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}
