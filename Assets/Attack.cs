using UnityEngine;

public class Attack : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().LoseHealth();
        }
    }
}