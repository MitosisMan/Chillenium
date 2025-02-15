using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    // Logic when interacting with stuff that'll affect player data
    public List<InteractableObj> list = new List<InteractableObj>();
    public Rigidbody2D rb;
    public PlayerMovement pm;
    public SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(list.Count > 0){
            InteractableObj closest = list[0];
            closest.active = false;
            float closestdistance = Vector2.Distance(closest.gameObject.transform.position, rb.position);
            for(int i = 1; i < list.Count; i++){
                list[i].active = false;
                float distance = Vector2.Distance(list[i].gameObject.transform.position, rb.position);
                if(distance < closestdistance){
                    closest = list[i];
                    closestdistance = distance;
                }
            }
            
            closest.active = true;
        }
    }
}
