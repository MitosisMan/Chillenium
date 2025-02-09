using UnityEngine;

public class HidingSpot : InteractableObj
{
    private Vector2 loc;
    private SpriteRenderer mysr;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite hiding;
    [SerializeField] public bool incutscene;
    [SerializeField] AudioSource hide;

    void Awake()
    {
        mysr = GetComponent<SpriteRenderer>();
    }

    public override void OnInteract()
    {
        if (!plint.pm.hiding)
        {
            plint.rb.velocity = new Vector2(0, 0);
            loc = plint.rb.position;
            plint.rb.position = transform.position;
            plint.pm.hiding = true;
            plint.sr.color = Color.clear;
            mysr.sprite = hiding;
            base.Popup.SetActive(false);

            if (incutscene)
            {
                StartCoroutine(GameObject.FindWithTag("Player").GetComponent<Cutscene>().Scene3());
            }
        }
        else
        {
            if (!incutscene)
            {
                plint.pm.hiding = false;
                plint.sr.color = Color.white;
                plint.rb.position = loc;
                mysr.sprite = normal;
                base.Popup.SetActive(true);
            }

        }
    }
}
