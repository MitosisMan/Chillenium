using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    RectTransform rm;
    public float rotationAmount = 5f; // Maximum rotation in degrees
    public float speed = 5f; // Speed of rotation

    void Start()
    {
        rm = GetComponent<RectTransform>();
    }

    void Update()
    {
        float rotation = Mathf.Sin(Time.time * speed) * rotationAmount;
        rm.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
