using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Vector2 moveDir = new(1,0);
    [SerializeField] private float speed = 3.5f;

    private void Update()
    {
        transform.Translate(moveDir * (speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            moveDir *= -1;
    }
}
