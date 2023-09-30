using UnityEngine;

public class HealOnPlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Player")) return;
        
        other.GetComponent<Health>().Heal();
    }
}
