using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBox : MonoBehaviour
{
    [SerializeField] HidingSpot bed;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !bed.incutscene)
        {
            StartCoroutine(GameObject.FindWithTag("Player").GetComponent<Cutscene>().Scene4());
        }
    }
}
