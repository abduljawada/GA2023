using UnityEngine;

public class Mutation : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.AddComponent<Fly>();
			Destroy(gameObject);
		}
	}
}
