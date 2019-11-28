using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public Animator animator;
    public LineRenderer line;
    public int ammo;
    public float radius;

    private bool waterlock = false;
    public SoundController soundController;

    public TextMeshProUGUI waterCount;

    private void Update()
    {
        waterCount.text = ammo.ToString();
    }

    public IEnumerator WaterTo(Transform from, Transform to)
    {
        if (waterlock)
        {
            yield break;
        }

        waterlock = true;
        animator.SetTrigger("Water");
        if (ammo <= 0)
        {
            waterlock = false;
            yield break;
        }

        ammo--;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(to.position, radius);

        yield return new WaitForSeconds(0.25f);

        line.SetPosition(0, from.position);
        line.SetPosition(1, to.position);
        line.enabled = true;
        yield return new WaitForSeconds(0.4f);
        soundController.PlayPumpingWaterSound();

        line.enabled = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Fire"))
            {
                colliders[i].gameObject.SetActive(false);
                break;
            }
        }

        waterlock = false;
    }
}
