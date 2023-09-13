using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Vector2 moveDir = new(1,0);
    [SerializeField] private float speed = 3.5f;

    private void Update()
    {
        transform.Translate(moveDir * (speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D()
    {
        moveDir *= -1;
        Flip();
    }
    
    private void Flip()
    {
        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }

}
