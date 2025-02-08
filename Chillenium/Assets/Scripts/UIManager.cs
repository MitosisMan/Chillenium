using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject minigame1;

    public void minigame(int num){
        if(num == 1){
            minigame1.SetActive(true);
        }
    }

    public void killminigame(){
        minigame1.SetActive(false);
    }
}
