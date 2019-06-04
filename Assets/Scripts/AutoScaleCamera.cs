using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleCamera : MonoBehaviour
{
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 3f / ((float)Screen.width / Screen.height);
    }

}
