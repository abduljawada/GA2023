using System;
using UnityEngine;

public class SpawnerInvoker : MonoBehaviour
{
    private Health Health => GetComponent<Health>();
    private float _originalPosX;
    private float _originalPosY;

    private void Start()
    {
        var position = transform.position;
        _originalPosX = position.x;
        _originalPosY = position.y;
    }

    public void InvokeSpawner()
    {
        Spawner.Singleton.Respawn(gameObject);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        Health.Reset();
        transform.position = new Vector2(_originalPosX, _originalPosY);
    }
}