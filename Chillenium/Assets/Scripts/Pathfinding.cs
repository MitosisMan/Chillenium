using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float sightRange;
    private float distance;
    private bool playerFound = false;
    private Vector3[] offsets = {Vector2.zero, new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, 0.5f), new Vector2(-0.5f, -0.5f)}; //need Vector3 to do math with transform.position
    private RaycastHit2D[] rays = new RaycastHit2D[5];

    void Start() {
    }

    void Update() {
        // update monster location to go towards player if we know location
        if (distance < sightRange && playerFound) {
            for (int i = 0; i < 5; i++) {
                if (rays[i].collider.gameObject == player) {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position - offsets[i], speed * Time.deltaTime); //move to player
                    break;
                }
            }
        } 
    }

    private void FixedUpdate() {
        distance = Vector2.Distance(player.transform.position, transform.position);
        Vector2[] directions = new Vector2[5];

        for (int i = 0; i < 5; i++) { // initialize 5 rays
            directions[i] = player.transform.position - offsets[i] - transform.position;
            rays[i] = Physics2D.Raycast(transform.position, directions[i]);
        }
        
        for (int i = 0; i < 5; i++) { // draw rays
            if (rays[i].collider != null) {
                playerFound = playerFound || rays[i].collider.gameObject == player; // true if at least one ray found player
                if (rays[i].collider.gameObject == player) {
                    Debug.DrawRay(transform.position, directions[i], Color.green);
                }
                else {
                    Debug.DrawRay(transform.position, directions[i], Color.red);
                }
            }
        }
    }
}