using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject minigame1;
    PlayerMovement pm;

    void Start(){
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void minigame(string reward){
        if(reward == "Scissors" && !pm.scissors){
            minigame1.SetActive(true);
            minigame1.GetComponent<Minigame1>().reward = reward;
        }
    }

    public void killminigame(){
        minigame1.SetActive(false);
    }
}
