using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    PlayerMovement pm;

    void Start(){
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            pm.paused = true;
            StartCoroutine(collision.gameObject.GetComponent<Pathfinding>().Kill());
        }
    }
    
}
