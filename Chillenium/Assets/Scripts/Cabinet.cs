using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : InteractableObj
{
    [SerializeField] UIManager ui;

    public override void OnInteract(){
        if(!plint.pm.minigaming){
            plint.rb.velocity = new Vector2(0, 0);
            plint.pm.minigaming = true;
            ui.minigame(1);
        }else{
            plint.pm.minigaming=false;
            ui.killminigame();
        }
    }
}
