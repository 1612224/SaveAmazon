using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricController : MonoBehaviour
{
    public Transform target;
    public int offset;

    private void Update()
    {
        if (target)
        {
            GetComponent<Renderer>().sortingOrder = -(int)(target.position.y * 100) + offset;
        }
        else
        {
            GetComponent<Renderer>().sortingOrder = -(int)(GetComponent<Transform>().position.y * 100) + offset;
        }
    }
}
