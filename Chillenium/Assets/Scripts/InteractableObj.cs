using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject Popup;
    public bool active = false;
    public PlayerInteractor plint;

    private void Start()
    {
        Popup.SetActive(false);
        plint = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInteractor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerInteractor>().enabled)
        {
            plint.list.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plint.list.Remove(this);
            active = false;
        }
    }

    void Update(){
        if(active){
            if(!plint.pm.hiding)
                Popup.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)){
                OnInteract();
            }
        }else if(plint.enabled && !plint.pm.hiding){
            Popup.SetActive(false);
        }
    }

    public virtual void OnInteract(){
    }
}
