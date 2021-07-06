using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebQueryDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PerformSimpleQuery());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PerformSimpleQuery()
    {
        // attempt to read from google
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.google.com"))
        {
            // timeout after 5 seconds
            request.timeout = 5;

            // yield until we get a response
            yield return request.SendWebRequest();

            // log out the response
            Debug.Log("Result was " + request.result);
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
