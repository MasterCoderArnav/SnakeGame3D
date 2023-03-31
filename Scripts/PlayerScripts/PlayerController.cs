using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector]
    public float step_length = 0.2f;

    [HideInInspector]
    public float movementFrequency = 0.1f;

    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> deltaPosition;
    private List<Rigidbody> nodes;

    private Rigidbody mainBody;
    private Rigidbody headBody;
    private Transform tr;

    private float counter;
    private bool move;
    private bool createNodeAtTail;

    private void Awake()
    {
        tr = transform;
        mainBody= GetComponent<Rigidbody>();
        initSnakeNodes();
        initPlayer();
        deltaPosition = new List<Vector3>()
        {
            new Vector3(-step_length, 0f), //-x
            new Vector3(0f, step_length), //y
            new Vector3(step_length, 0f), //x
            new Vector3(0f, -step_length) //-y
        };
    }

    private void initSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        headBody = nodes[0];
    }

    private void setDirectionRandom()
    {
        int dir = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dir;
    }

    private void initPlayer()
    {
        setDirectionRandom();
        switch(direction)
        {
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0.0f, 0.0f);
                nodes[2].position = nodes[0].position + new Vector3(2f * Metrics.NODE, 0.0f, 0.0f);
                break;
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0.0f, 0.0f);
                nodes[2].position = nodes[0].position - new Vector3(2f * Metrics.NODE, 0.0f, 0.0f);
                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0.0f, Metrics.NODE, 0.0f);
                nodes[2].position = nodes[0].position - new Vector3(0.0f, 2f * Metrics.NODE, 0.0f);
                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0.0f, Metrics.NODE, 0.0f);
                nodes[2].position = nodes[0].position + new Vector3(0.0f, 2f * Metrics.NODE, 0.0f);
                break;
        }
    }

    void Move()
    {
        Vector3 dPos = deltaPosition[(int)direction];
        Vector3 parentPos = headBody.position;
        Vector3 prevPos;
        headBody.position = headBody.position + dPos;
        mainBody.position = mainBody.position + dPos;

        for(int i = 1; i < nodes.Count; i++)
        {
            prevPos = nodes[i].position;
            nodes[i].position = parentPos;
            parentPos = prevPos;
        }

        if (createNodeAtTail)
        {
            createNodeAtTail = false;
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count-1].position, Quaternion.identity);
            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
        }
    }

    private void Update()
    {
        checkMovementFrequency();
    }

    private void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }
    void checkMovementFrequency()
    {
        counter += Time.deltaTime;
        if (counter >= movementFrequency)
        {
            counter = 0;
            move = true;
        }
    }

    public void setInputDirection(PlayerDirection dir)
    {
        if ((direction == PlayerDirection.LEFT && dir == PlayerDirection.RIGHT) || (direction == PlayerDirection.RIGHT && dir == PlayerDirection.LEFT) || (direction == PlayerDirection.UP && dir == PlayerDirection.DOWN) || (direction == PlayerDirection.DOWN && dir == PlayerDirection.UP))
        {
            return;
        }
        direction = dir;
        forceMove();
    }

    private void forceMove()
    {
        counter = 0;
        move = false;
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.WALL) || other.CompareTag(Tags.BOMB) || other.CompareTag(Tags.TAIL))
        {
            AudioManager.instance.gameOverTheme();
            Time.timeScale = 0;
        }
        else if (other.CompareTag(Tags.FRUIT))
        {
            Debug.Log("Collided with Fruit");
            createNodeAtTail = true;
            Destroy(other.gameObject);
            AudioManager.instance.fruitPickUp();
            GameplayController.instance.increaseScore();
        }
    }
}
