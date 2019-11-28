using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Animator animator;
    public Slider healthBar;

    private int health = 100;
    private bool animationPlayed = false;

    private void Start()
    {
        healthBar.value = health;
    }

    public void Damage(int hitpoint)
    {
        health -= hitpoint;
        if (health < 0)
        {
            health = 0;
            animator.SetBool("IsDead", true);
            if (!animationPlayed)
            {
                animator.Play("Fall", -1, 0f);
                animationPlayed = true;
            }
        }
        healthBar.value = health;
    }
}
