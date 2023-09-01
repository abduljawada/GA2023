using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent @event;
    private void OnTriggerEnter2D(Collider2D other)
    {
        @event?.Invoke();
    }
}