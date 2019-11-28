using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Collider2D checkCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyOnFire destroyOnFire = collision.gameObject.GetComponent<DestroyOnFire>();
        if (destroyOnFire)
        {
            destroyOnFire.Destroy();
        }
        DamageOnFire damageOnFire = collision.gameObject.GetComponent<DamageOnFire>();
        if (damageOnFire)
        {
            damageOnFire.isOnFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DamageOnFire damageOnFire = collision.gameObject.GetComponent<DamageOnFire>();
        if (damageOnFire)
        {
            damageOnFire.isOnFire = false;
        }
    }

    private void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        checkCollider.OverlapCollider(new ContactFilter2D(), colliders);
        for (int i = 0; i < colliders.Count; i++)
        {
            DestroyOnFire destroyOnFire = colliders[i].gameObject.GetComponent<DestroyOnFire>();
            if (destroyOnFire)
            {
                destroyOnFire.Destroy();
            }
            DamageOnFire damageOnFire = colliders[i].gameObject.GetComponent<DamageOnFire>();
            if (damageOnFire)
            {
                damageOnFire.isOnFire = true;
            }
        }
    }
}
