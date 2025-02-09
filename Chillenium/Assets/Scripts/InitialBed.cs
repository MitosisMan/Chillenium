using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBed : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GameObject.FindWithTag("Player").GetComponent<Cutscene>().Scene2());
        }
    }
}
