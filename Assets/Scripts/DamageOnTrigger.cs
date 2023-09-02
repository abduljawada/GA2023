using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Fix Console Errors
        //Debug.Log(name + " hitting " + other.name);
        other.GetComponent<Health>().LoseHealth();
    }
}
