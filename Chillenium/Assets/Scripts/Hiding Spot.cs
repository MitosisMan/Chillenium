using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractableObj
{
    public override void OnInteract(){
        if(!plint.pm.hiding){
            plint.rb.velocity = new Vector2(0, 0);
            plint.rb.position = transform.position;
            plint.pm.hiding = true;
        }else{
            plint.pm.hiding=false;
        }
    }
}
