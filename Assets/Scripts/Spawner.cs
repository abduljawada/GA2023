using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public static Spawner Singleton;
	[SerializeField] private float timeToRespawn = 30f;

	private void Awake()
	{
		Singleton = this;
	}

	public void Respawn(GameObject objectToRespawn)
	{
		StartCoroutine(RespawnCoroutine(objectToRespawn));
	}
	private IEnumerator RespawnCoroutine(GameObject objectToRespawn)
	{
		yield return new WaitForSeconds(timeToRespawn);
		objectToRespawn.SetActive(true);
		objectToRespawn.GetComponent<SpawnerInvoker>().Reset();
	}
}
