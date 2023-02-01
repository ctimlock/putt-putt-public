using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicGameEventRaiser : MonoBehaviour
{
    public GameEvent Event;

    public void Start()
    {
        StartCoroutine(RaiseEventPeriodically());
    }

    public IEnumerator RaiseEventPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            Event.Raise();
            yield return null;
        }
    }
}
