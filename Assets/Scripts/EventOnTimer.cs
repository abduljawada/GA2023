using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTimer : MonoBehaviour
{
    [SerializeField] private UnityEvent onTimerEnd;
    
    public void StartTimer(float time)
    {
        StartCoroutine(TimerCoroutine(time));
    }

    private IEnumerator TimerCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        onTimerEnd?.Invoke();
    }
}
