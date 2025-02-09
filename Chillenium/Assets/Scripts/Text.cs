using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    TMPro.TextMeshProUGUI textImg;
    public float startTime;
    int ind = 0;
    public string s = "";

    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        textImg = GetComponent<TMPro.TextMeshProUGUI>();
        s = textImg.text;
        textImg.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        ind = (int)((Time.time - startTime) * 15);
        if (ind > s.Length)
        {
            ind = s.Length;
        }
        textImg.text = s.Substring(0, ind);
    }
}
