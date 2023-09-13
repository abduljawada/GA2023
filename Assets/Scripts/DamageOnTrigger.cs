using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask toDamageLayerMask;
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((toDamageLayerMask.value & (1 << other.gameObject.layer)) != 0)
        { 
            other.GetComponent<Health>().LoseHealth(damage);
        }
    }
}
