using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnFire : MonoBehaviour
{
    public int damage = 10;
    public float damageInterval = 1f;
    public GameObject fire;
    public bool isOnFire;

    private float accumulatedTime = 0f;

    private void Start()
    {
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;
        HealthController hc = GetComponent<HealthController>();
        if (DamageCircle.IsOutSideCircle(transform.position) || isOnFire)
        {
            fire.SetActive(true);
            if (accumulatedTime > damageInterval)
            {
                accumulatedTime = 0;
                hc.Damage(10);
            }
        }
        else
        {
            fire.SetActive(false);
        }
    }
}
