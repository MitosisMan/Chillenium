using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement pm;
    private Rigidbody2D rb;
    private Rigidbody2D thisrb;
    public float speed;
    public float sightRange;
    private bool looking = false;
    private Vector3[] offsets = {
        Vector2.zero, new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), 
        new Vector2(-0.5f, 0.5f), new Vector2(-0.5f, -0.5f)
    };
    float memint = 0;

    private RaycastHit2D[] rays = new RaycastHit2D[5];
    [SerializeField] private TileTest tiles;
    [SerializeField] GameObject[] checkpoints;
    int checkpointCount;
    List<PathFinderNode> path;

    private SpriteRenderer sr;
    private int intdirection = 0;
    [SerializeField] private Sprite[] sprites;
    private float animationSpeed = 0.15f; // Time per frame
    private int frameIndex = 0; // Tracks animation frame

    [SerializeField] private Sprite[] killsprites;
    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject dialogueText;

    int offsetx = 37;
    int offsety = 12;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        thisrb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pm = player.GetComponent<PlayerMovement>();
    }

    void Update(){
    if (tiles != null && tiles.grid != null)
    {
        Vector2 start = new Vector2(Mathf.RoundToInt(transform.position.x) + offsetx, Mathf.RoundToInt(transform.position.y) + offsety); // Offset
        Vector2 end;
        if(memint > 0){
            end = new Vector2(Mathf.RoundToInt(rb.position.x) + offsetx, Mathf.RoundToInt(rb.position.y) + offsety);
        }else{
            end = new Vector2(Mathf.RoundToInt(checkpoints[checkpointCount].transform.position.x) + offsetx, Mathf.RoundToInt(checkpoints[checkpointCount].transform.position.y) + offsety); // Offset
        }

        path = FindPath(start, end, tiles.grid);

        if (path != null && path.Count > 1)
        {
            Vector2 nextStep = path[1].Position - (Vector2)transform.position;
            nextStep.x -= offsetx;
            nextStep.y -= offsety; // Offset
            thisrb.velocity = nextStep.normalized * speed * (pm.paused ? 0 : 1);
        }
        else
        {
            if(!looking)
                StartCoroutine(LookAround());
            thisrb.velocity = Vector2.zero; // Stop if no path found or already at destination
        }

        UpdateDirection();
        if(!pm.paused)
            AnimateSprite();
    }
}

    public IEnumerator Kill(){
        pm.paused = true;
        if(intdirection == 0){
            if(rb.position.x > thisrb.position.x){
                sr.flipX = true;
            }else{
                sr.flipX = false;
            }
            for(int i = 0; i < 9; i++){
                sr.sprite = killsprites[i];
                yield return new WaitForSeconds(.1f);
                if(i%9 == 3){
                    blackout.SetActive(true);
                }
            }
        }else if(intdirection > 0){
            if(intdirection == 2){
                sr.flipX = true;
            }else{
                sr.flipX = false;
            }
            for(int i = 9; i < 18; i++){
                sr.sprite = killsprites[i];
                yield return new WaitForSeconds(.1f);
                if(i%9 == 3){
                    blackout.SetActive(true);
                }
            }
        }

        dialogue.SetActive(true);
        dialogueText.SetActive(true);
    }

    private IEnumerator LookAround(){
        looking = true;
        yield return new WaitForSeconds(2f);
        checkpointCount++;
        if(checkpointCount >= checkpoints.Length){
            checkpointCount = 0;
        }
        looking = false;
    }

    private void UpdateDirection()
    {
        if (thisrb.velocity.magnitude > 0) // Only update direction if moving
        {
            if (Mathf.Abs(thisrb.velocity.x) > -1 * thisrb.velocity.y)
            {
                intdirection = (thisrb.velocity.x > 0) ? 2 : 1; // Right = 2, Left = 1
            }
            else
            {
                intdirection = 0; // Down = 0
            }
        }
    }

    private void AnimateSprite()
    {
        if(intdirection == 2){
            frameIndex = (int)(Time.time / (animationSpeed * 2)) % 6; // Loops 0-3
            int spriteIndex = 15 + frameIndex; // Selects the correct sprite
            sr.sprite = sprites[spriteIndex];
        }else if(intdirection == 1){
            frameIndex = (int)(Time.time / (animationSpeed * 2)) % 6; // Loops 0-3
            int spriteIndex = 9 + frameIndex; // Selects the correct sprite
            sr.sprite = sprites[spriteIndex];
        }else if(intdirection == 0){
            if(rb.position.x > thisrb.position.x){
                sr.flipX = true;
            }else{
                sr.flipX = false;
            }
            frameIndex = (int)(Time.time / (animationSpeed * 2)) % 9; // Loops 0-3
            int spriteIndex = frameIndex; // Selects the correct sprite
            sr.sprite = sprites[spriteIndex];
        }
    }

    private void FixedUpdate()
    {
        memint -= Time.deltaTime;
        speed = 2;

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position - (Vector2)offsets[i];
            rays[i] = Physics2D.Raycast(transform.position, direction.normalized, sightRange);

            if (rays[i].collider != null)
            {
                if (rays[i].collider.gameObject == player && (!pm.hiding || memint > 0))
                {
                    memint = 2;
                    speed = 5;
                    Debug.DrawRay(transform.position, direction, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, direction, Color.red);
                }
            }
        }
    }

    // A* Pathfinding Implementation with a Priority Queue
    public List<PathFinderNode> FindPath(Vector2 start, Vector2 end, int[,] grid)
    {
        int gridWidth = grid.GetLength(0);
        int gridHeight = grid.GetLength(1);

        PriorityQueue<PathFinderNode> openSet = new PriorityQueue<PathFinderNode>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, PathFinderNode> allNodes = new Dictionary<Vector2Int, PathFinderNode>();

        PathFinderNode startNode = new PathFinderNode(start, null, 0, Heuristic(start, end));
        openSet.Enqueue(startNode);
        allNodes[Vector2Int.RoundToInt(start)] = startNode;

        while (openSet.Count > 0)
        {
            PathFinderNode currentNode = openSet.Dequeue();

            if (currentNode.Position == end)
            {
                return RetracePath(currentNode);
            }

            closedSet.Add(Vector2Int.RoundToInt(currentNode.Position));

            foreach (Vector2Int neighbor in GetNeighbors(Vector2Int.RoundToInt(currentNode.Position), gridWidth, gridHeight))
            {
                if (grid[neighbor.x, neighbor.y] == 0 || closedSet.Contains(neighbor))
                    continue;

                float newGCost = currentNode.GCost + 1;

                if (!allNodes.TryGetValue(neighbor, out PathFinderNode neighborNode))
                {
                    neighborNode = new PathFinderNode(neighbor, currentNode, newGCost, Heuristic(neighbor, end));
                    allNodes[neighbor] = neighborNode;
                    openSet.Enqueue(neighborNode);
                }
                else if (newGCost < neighborNode.GCost)
                {
                    neighborNode.Parent = currentNode;
                    neighborNode.GCost = newGCost;
                    openSet.UpdatePriority(neighborNode);
                }
            }
        }

        return new List<PathFinderNode>(); // No path found
    }

    private List<PathFinderNode> RetracePath(PathFinderNode endNode)
    {
        List<PathFinderNode> path = new List<PathFinderNode>();
        PathFinderNode currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private float Heuristic(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan Distance
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position, int width, int height)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions = {
            new Vector2Int(0, 1), new Vector2Int(1, 0), 
            new Vector2Int(0, -1), new Vector2Int(-1, 0)
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = position + dir;

            if (neighborPos.x >= 0 && neighborPos.x < width && neighborPos.y >= 0 && neighborPos.y < height)
                neighbors.Add(neighborPos);
        }

        return neighbors;
    }
}

// PathFinderNode class to store path information
public class PathFinderNode : System.IComparable<PathFinderNode>
{
    public Vector2 Position;
    public PathFinderNode Parent;
    public float GCost; // Distance from start node
    public float HCost; // Heuristic distance to target
    public float FCost => GCost + HCost; // Total estimated cost

    public PathFinderNode(Vector2 position, PathFinderNode parent, float gCost, float hCost)
    {
        Position = position;
        Parent = parent;
        GCost = gCost;
        HCost = hCost;
    }

    public int CompareTo(PathFinderNode other)
    {
        return FCost.CompareTo(other.FCost);
    }
}

// Priority Queue for efficient pathfinding
public class PriorityQueue<T> where T : System.IComparable<T>
{
    private List<T> heap = new List<T>();

    public int Count => heap.Count;

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (heap[i].CompareTo(heap[parent]) >= 0) break;
            (heap[i], heap[parent]) = (heap[parent], heap[i]);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (heap.Count == 0) return default;

        T root = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int i = 0;
        while (true)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int smallest = i;

            if (left < heap.Count && heap[left].CompareTo(heap[smallest]) < 0) smallest = left;
            if (right < heap.Count && heap[right].CompareTo(heap[smallest]) < 0) smallest = right;
            if (smallest == i) break;

            (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
            i = smallest;
        }

        return root;
    }

    public void UpdatePriority(T item)
    {
        int index = heap.IndexOf(item);
        if (index >= 0) Enqueue(Dequeue());
    }
}
