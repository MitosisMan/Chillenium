using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4.0f;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    public bool hiding = false;
    public bool minigaming = false;
    public bool scissors, item2, item3, item4, item5 = false;
    [SerializeField] GameObject scissorsHitbox;
    [SerializeField] GameObject scissorsPopup;
    [SerializeField] AnimationController animationController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movement Logic
        if(!hiding && !minigaming){
            dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dir = Vector3.Normalize(dir);
            rb.velocity = dir * speed;
        }
    }

    public void GainObject(string item){
        minigaming = false;
        if(item == "Scissors"){
            Destroy(scissorsHitbox);
            Destroy(scissorsPopup);
            scissors = true;
        }
    }
}
