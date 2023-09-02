using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Vector2 moveDir = new(1,0);
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private bool isFacingLeft = true;

    private void Update()
    {
        transform.Translate(moveDir * (speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // change position of movement
	    moveDir *= -1;

        if (moveDir.x > 0 && isFacingLeft) 
            Flip();

        else if (moveDir.x < 0 && !isFacingLeft) 
            Flip();
    }

    // Flip sprite of object when colliding with obstacle
    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
