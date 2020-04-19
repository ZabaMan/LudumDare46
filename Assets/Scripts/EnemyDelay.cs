using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDelay : MonoBehaviour
{
    public Behaviour componentToDelay;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        componentToDelay.enabled = true;
    }
}
