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

  
    void OnCollisionEnter (UnityEngine.Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            gameManager.ItemPickedUp();
        }
    }
}
