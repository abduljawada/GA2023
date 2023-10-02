using UnityEngine;

public class Fly : MonoBehaviour
{
	private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

	private const float FlyForce = 3f;
	private const float MaxFlySpeed = 20f;
	private bool _willFly;	
	private bool _isFlying;

	private void Update()
	{
		_willFly = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
		if (!_willFly) _isFlying = false;
	}
	
	private void FixedUpdate()
	{
		if (!_willFly || Rigidbody2D.velocity.y > 0 && !_isFlying) return;
		
		_isFlying = true;
		
		Rigidbody2D.AddForce(Vector2.up * FlyForce, ForceMode2D.Impulse);
		
		if (Rigidbody2D.velocity.y > MaxFlySpeed)
		{
			Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, MaxFlySpeed);
		}
	}
}
