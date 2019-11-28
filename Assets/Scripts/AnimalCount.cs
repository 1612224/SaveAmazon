using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimalCount : MonoBehaviour
{
    private int animalCount = 0;
   
    public Collider2D checkCollider;
    public CarryingObject player;
    public int totalAnimal;

    public TextMeshProUGUI total;
    public TextMeshProUGUI current;

    private void Start()
    {
        total.text = totalAnimal.ToString();
    }

    private void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        checkCollider.OverlapCollider(new ContactFilter2D(), colliders);
        for (int i = 0; i < colliders.Count; i++)
        {
            GameObject beingCarriedObject = player.currentObject ? player.currentObject.gameObject : null;
            if (colliders[i].CompareTag("Animal") && !Object.Equals(colliders[i].gameObject, beingCarriedObject))
            {
                colliders[i].gameObject.SetActive(false);
                animalCount++;
                current.text = animalCount.ToString();
            }
        }
    }
}
