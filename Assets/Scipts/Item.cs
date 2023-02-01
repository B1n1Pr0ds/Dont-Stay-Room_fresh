using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.GetComponent<Collider>().tag == "Player")
        {
            Destroy(gameObject);
            gameManager.ItemPickedUp();
        }
    }
}
