using UnityEngine;

public class Fly : MonoBehaviour
{
	private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

	private const float FlyForce = 3f;
	private float _flyTime = 5f;
	private const float MaxFlySpeed = 20f;
	private bool _willFly;	

	private void Update()
	{
		_flyTime -= Time.deltaTime;

		if(_flyTime <= 0) Destroy(this);

		_willFly = Input.GetKey(KeyCode.Space);
	}
	
	private void FixedUpdate()
	{
		if (!_willFly) return;
		Rigidbody2D.AddForce(Vector2.up * FlyForce, ForceMode2D.Impulse);
		if (Rigidbody2D.velocity.y > MaxFlySpeed)
		{
			Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, MaxFlySpeed);
		}
	}
}
