using UnityEngine;

public class Fly : MonoBehaviour
{
	private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
	
	[SerializeField] private float flyForce = 3f;
	[SerializeField] private float flyTime = 10f;
	[SerializeField] private float maxFlySpeed = 20f;
	
	private void Update()
	{
		flyTime -= Time.deltaTime;

		if(flyTime <= 0) Destroy(this);

		if (Input.GetKey(KeyCode.Space))
		{
			Rigidbody2D.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
			if (Rigidbody2D.velocity.y > maxFlySpeed)
			{
				Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxFlySpeed);
			}
		}
	}
}
