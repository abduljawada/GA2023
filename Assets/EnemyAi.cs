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
        // Upon colliding with the player inflict damage and change position
        if (other.gameObject.CompareTag("Player"))
	    {
            other.gameObject.GetComponent<Health>().LoseHealth();
            moveDir *= -1;
	    }
        // Upon colliding with an obstacle, changes position
	    else if (other.gameObject.CompareTag("Obstacle"))
	    {
            moveDir *= -1;
	    }
    }
}
