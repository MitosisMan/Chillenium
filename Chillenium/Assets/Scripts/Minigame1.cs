using UnityEngine;
using UnityEngine.UI;

public class Minigame1 : MonoBehaviour
{
    [SerializeField] private Slider slide;  // The bar slider
    [SerializeField] private Slider progress;  // The bar slider
    [SerializeField] private RectTransform hookArea;  // UI element representing the catch zone
    [SerializeField] private Slider fish;  // UI element representing the fish
    [SerializeField] private float hookSpeed = .8f;  // Speed of player-controlled hook
    [SerializeField] private float successTime = 3f;  // Time required to catch the fish
    [SerializeField] public string reward;

    private float velocity = 0;
    private float gravity = -1f;
    private float catchProgress = 0f;
    private float fishDirection = 1;

    private void Update()
    {
        HandleHookMovement();
        HandleFishMovement();
        CheckCatchCondition();
    }

    private void HandleHookMovement()
    {
        // Move hook up or down based on player input
        if (Input.GetKey(KeyCode.Space))
        {
            velocity += hookSpeed * Time.deltaTime;
        }

        if(slide.value == 100){
            velocity = 0;
        }

        // Apply gravity
        velocity += gravity * Time.deltaTime;
        velocity = Mathf.Min(velocity, .3f);

        slide.value = Mathf.Clamp(slide.value + velocity, 0, 100);
        if(slide.value == 0){
            velocity = 0;
        }
    }

    private void HandleFishMovement()
    {
        // Move fish up and down automatically
        fish.value += fishDirection * Time.deltaTime * 50;

        // Reverse direction if hitting bounds
        if ((fish.value > 99 && fishDirection > 0) || (fish.value < 1 && fishDirection < 0))
        {
            fishDirection *= -1;
        }else if(Random.Range(0f, 350f) <= 1){
            fishDirection *= -1;
        }
    }

    private void CheckCatchCondition()
    {
        // Check if fish is inside hook area
        if (Mathf.Abs(fish.value - slide.value) < 25f) // Using a relative threshold
        {
            catchProgress += Time.deltaTime;
            if (catchProgress >= successTime)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().GainObject(reward);
                gameObject.SetActive(false);
            }
        }
        else
        {
            catchProgress = Mathf.Max(0, catchProgress - Time.deltaTime);
        }
        progress.value = catchProgress / successTime;
    }
}
