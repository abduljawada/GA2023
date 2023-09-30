using UnityEngine;

public class EventOnPlayerTrigger : EventOnTrigger
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Player")) return;
        
        base.OnTriggerEnter2D(other);
    }
}
