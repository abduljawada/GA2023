using UnityEngine;

public class EventChangeSave : MonoBehaviour
{
    [SerializeField] private SaveData saveData;

    public void UpdateSave()
    {
        saveData.spawnPos = transform.position;
    }
}
