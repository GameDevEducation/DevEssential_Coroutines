using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDemo : MonoBehaviour
{
    [SerializeField] float SpinDelay = 5f;
    [SerializeField] float DelayPerTarget = 2f;
    [SerializeField] float SpinTime = 10f;
    [SerializeField] List<GameObject> SpinTargets;

    [SerializeField] bool ForceStopCoroutines = false;

    // Start is called before the first frame update
    void Start()
    {
        // launch the coroutine passing it the value of SpinDelay
        float workingDelay = SpinDelay;
        foreach(var target in SpinTargets)
        {
            StartCoroutine(PerformSpin(target, workingDelay));

            workingDelay += DelayPerTarget;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ForceStopCoroutines)
        {
            ForceStopCoroutines = false;

            // stop ALL coroutines started by this game object
            StopAllCoroutines();
        }
    }

    IEnumerator PerformSpin(GameObject target, float delay)
    {
        // tell the coroutine to pause until delay amount time has passed
        yield return new WaitForSeconds(delay);

        // loop while spin progress is less than 100%
        float spinProgress = 0f;
        while (spinProgress < 1f)
        {
            // update the spin progress
            spinProgress += Time.deltaTime / SpinTime;

            // update the rotation
            target.transform.eulerAngles = new Vector3(0f, 360f * spinProgress, 0f);

            // tell the coroutine to pause until the end of the frame
            yield return new WaitForEndOfFrame();
        }
    }
}
