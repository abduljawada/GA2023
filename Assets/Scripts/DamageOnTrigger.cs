using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask toDamageLayerMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((toDamageLayerMask.value & (1 << other.gameObject.layer)) != 0)
        { 
            other.GetComponent<Health>().LoseHealth();
        }
        //TODO: Fix Console Errors
        //Debug.Log(name + " hitting " + other.name);
    }
}
