using UnityEngine;

public class MutationObject : MonoBehaviour
{
	[SerializeField] private MutationData mutationData;
	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<MutationManager>().AddMutation(gameObject, mutationData);
		}
	}
}
