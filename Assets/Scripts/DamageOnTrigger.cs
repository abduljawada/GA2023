using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Check if other's layer is in collision layer mask
        Debug.Log(name + " hitting " + other.name);
        other.GetComponent<Health>().LoseHealth();
    }
}
