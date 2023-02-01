using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter()
    {
        Debug.Log("Collided");
    }
}
   
