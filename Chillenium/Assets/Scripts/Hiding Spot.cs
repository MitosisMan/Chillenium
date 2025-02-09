using UnityEngine;

public class HidingSpot : InteractableObj
{
    private Vector2 loc;
    private SpriteRenderer mysr;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite hiding;

    void Awake(){
        mysr = GetComponent<SpriteRenderer>();
    }

    public override void OnInteract(){
        if(!plint.pm.hiding){
            plint.rb.velocity = new Vector2(0, 0);
            loc = plint.rb.position;
            plint.rb.position = transform.position;
            plint.pm.hiding = true;
            plint.sr.color = Color.clear;
            mysr.sprite = hiding;
            base.Popup.SetActive(false);
        }else{
            plint.pm.hiding=false;
            plint.sr.color = Color.white;
            plint.rb.position = loc;
            mysr.sprite = normal;
            base.Popup.SetActive(true);
        }
    }
}
