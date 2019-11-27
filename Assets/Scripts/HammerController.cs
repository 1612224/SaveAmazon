using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    public float radius;
    public Animator animator;

    public IEnumerator HammerTo(Transform to)
    {
        animator.Play("Hammer", -1, 0f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(to.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] && colliders[i].CompareTag("Obstacle"))
            {
                yield return new WaitForSeconds(0.5f);
                colliders[i].gameObject.SetActive(false);
                yield break;
            }
        }
    }
}
