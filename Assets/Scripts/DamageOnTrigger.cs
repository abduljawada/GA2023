using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + " hitting " + other.name);
        other.GetComponent<Health>().LoseHealth();
    }
}
