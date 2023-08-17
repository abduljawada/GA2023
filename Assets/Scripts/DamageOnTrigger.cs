using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private string otherTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(otherTag))
        {
            other.GetComponent<Health>().LoseHealth();
        }
    }
}
