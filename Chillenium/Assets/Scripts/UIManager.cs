using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject minigame1;
    PlayerMovement pm;

    void Start(){
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void minigame(string reward){
        if(reward == "Stick" && !pm.stick){
            minigame1.SetActive(true);
            minigame1.GetComponent<Minigame1>().reward = reward;
        }
        if(reward == "Ball" && !pm.ball){
            minigame1.SetActive(true);
            minigame1.GetComponent<Minigame1>().reward = reward;
        }
        if(reward == "Band" && !pm.band){
            minigame1.SetActive(true);
            minigame1.GetComponent<Minigame1>().reward = reward;
        }
    }

    public void killminigame(){
        minigame1.SetActive(false);
    }
}
