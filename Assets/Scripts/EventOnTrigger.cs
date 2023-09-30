using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    [SerializeField] protected UnityEvent @event;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        @event?.Invoke();

        if (gameObject.tag.Equals("gem"))
        {
            FindObjectOfType<GemCollect>().AddGem();
        }
    }
}   