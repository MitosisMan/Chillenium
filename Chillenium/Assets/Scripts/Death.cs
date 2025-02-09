using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    PlayerMovement pm;
    [SerializeField] AudioSource grab;

    void Start(){
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            pm.paused = true;
            grab.Play();
            StartCoroutine(collision.gameObject.GetComponent<Pathfinding>().Kill());
        }
    }
    
}
