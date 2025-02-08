using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractableObj
{
    BoxCollider2D box;
    void Start(){
        box = GetComponent<BoxCollider2D>();
    }

    public override void OnInteract(){
        
    }
}
