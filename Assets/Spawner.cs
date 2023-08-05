using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private Transform[] transforms;
	private Vector2[] _positions;
	[SerializeField] private float spawnCheckInterval = 60f;
	private float _timeTillNextCheck;

	private void Awake()
	{
		_positions = new Vector2[transforms.Length];
		for(var i = 0; i < transforms.Length; i++)
		{
			_positions[i] = transforms[i].position;
		}
	}

    private void Start()
    {
       _timeTillNextCheck = spawnCheckInterval; 
    }

    private void Update()
    {
       _timeTillNextCheck -= Time.deltaTime;
       if (!(_timeTillNextCheck <= 0)) return;
       for(var i = 0; i < transforms.Length; i++)
       {
	       if(!transforms[i])
	       {
		       transforms[i] = Instantiate(enemyPrefab, _positions[i], Quaternion.identity).transform;
	       }
       }

       _timeTillNextCheck = spawnCheckInterval;
    }
}
