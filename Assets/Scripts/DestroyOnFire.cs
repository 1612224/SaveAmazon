using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFire : MonoBehaviour
{
    public CarryingObject carrier;

    public void Destroy()
    {
        if (carrier == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GameObject beingCarried = carrier.currentObject ? carrier.currentObject.gameObject : null;
            if (!Object.Equals(beingCarried, gameObject))
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DamageCircle.IsOutSideCircle(transform.position))
        {
            gameObject.SetActive(false);
        }
    }
}
