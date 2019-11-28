using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeHide : MonoBehaviour
{
    [Range(0,1)]
    public float transparentPercentage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform colliderTransform = collision.transform;
        if (collision.CompareTag("Tree") && colliderTransform.position.y  <= transform.position.y)
        {
            MakeTransparent(collision.GetComponent<Renderer>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            MakeOpaque(collision.GetComponent<Renderer>());
        }
    }

    private void MakeTransparent(Renderer renderer)
    {
        for(int i = 0; i < renderer.materials.Length; i++)
        {
            Material mat = renderer.materials[i];
            Color finalColor = mat.color;
            finalColor.a = 1 - transparentPercentage;
            mat.color = finalColor;
        }
    }

    private void MakeOpaque(Renderer renderer)
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            Material mat = renderer.materials[i];
            Color finalColor = mat.color;
            finalColor.a = 1;
            mat.color = finalColor;
        }
    }
}
