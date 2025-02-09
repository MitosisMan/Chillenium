using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    PlayerMovement pm;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject dialogueText;
    PlayerInteractor plint;


    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pm.paused = true;
        plint = GetComponent<PlayerInteractor>();
        plint.enabled = false;
        StartCoroutine(Intro());
    }

    IEnumerator Intro(){
        yield return new WaitForSeconds(1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().s = "Timmy lay in bed one night dreaming a pleasant dream.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "All of a sudden, a loud CRASH sounded throughout the house.";
        yield return new WaitForSeconds(5f);
        
        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Timmy quickly pressed WASD to go investigate.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        pm.paused = false;
    }

    public IEnumerator Scene2(){
        pm.paused = true;
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Suddenly, a horrible feeling overcame Timmy.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "He knew, against all logic, there was a Monster here.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Timmy, resourceful as ever, went to his bed and pressed E to hide.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        pm.paused = false;

        plint.enabled = true;
        Destroy(GameObject.FindWithTag("Bed").GetComponent<InitialBed>());
    }

    public IEnumerator Scene3(){
        Rigidbody2D rb = GameObject.FindWithTag("Monster").GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(3, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(-3, 0);
        yield return new WaitForSeconds(2f);

        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "With the monster gone, Timmy was free to press E once again.";
        yield return new WaitForSeconds(5f);

        GameObject.FindWithTag("Bed").GetComponent<HidingSpot>().incutscene = false;
        dialogue.SetActive(false);
        dialogueText.SetActive(false);
    }

    public IEnumerator Scene4(){
        pm.paused = true;

        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Timmy had to protect his home from this Monster.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "He had a plan to build a weapon to scare off this Monster.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "First he would need to gather supplies from around the house.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Thankfully, the first thing Timmy needed was right here in his room.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Timmy walked up to the cabinet and pressed E to try to get it open.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        pm.paused = false;
    }

    public IEnumerator Scene5(){
        pm.paused = true;

        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "Now that he had the first piece of his weapon, Timmy had to leave his room.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogue.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().startTime = Time.time;
        dialogueText.GetComponent<Text>().s = "He knew that the next two pieces, the rubber band and stick, would be around his house.";
        yield return new WaitForSeconds(5f);

        dialogue.SetActive(false);
        dialogueText.SetActive(false);
        pm.paused = false;
        SceneManager.LoadScene("House");
    }
}
