using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CarryingObject : MonoBehaviour
{
    public Transform currentObject;
    public Vector2 offset;
    private Transform currentPosition;

    // animation
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject)
        {
            currentObject.position = new Vector3(currentPosition.position.x + offset.x, currentPosition.position.y + offset.y);
        }
    }

    public bool IsCarrying()
    {
        return currentObject != null;
    }

    public void Pickup(Transform obj)
    {
        currentObject = obj;
        obj.gameObject.GetComponent<IsometricController>().target = currentPosition;
        obj.gameObject.GetComponent<IsometricController>().offset = 1;
        animator.SetBool("IsCarrying", true);
    }
    
    public void Throw(Vector2 throwPosition)
    {
        currentObject.position = throwPosition;
        currentObject.gameObject.GetComponent<IsometricController>().target = null;
        currentObject = null;
        animator.SetBool("IsCarrying", false);
    }
}
