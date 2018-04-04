using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    private int lives;

    [SerializeField]
    Animation sphereAnimation;

    Node[,,] nodes;
    Vector3 targetPosition;

    Vector3 floorInput;
    Vector3 prevFloorInput;

    public delegate void OnFloorInput(Vector3 newVal);
    public event OnFloorInput OnVariableChange;

    float lerpSpeed = 20f;

	// Use this for initialization
	void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        nodes = GameManager.GetNodes();

        lives = 3;

        OnVariableChange += OnFloorInputDetected;

        targetPosition = Coordinate.ToVector(GameManager.nodes[1, 1, 1].Position);
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        floorInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical2"), Input.GetAxisRaw("Vertical")).normalized;
        
        if (floorInput.magnitude != prevFloorInput.magnitude && OnVariableChange != null)
        {
            prevFloorInput = floorInput;

            if (prevFloorInput.magnitude != 0)
                OnVariableChange(floorInput);
        }

        Move(targetPosition);

    }

    private void Move(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, (target - Vector3.one) * 2, Time.deltaTime * lerpSpeed);
    }

    void OnFloorInputDetected(Vector3 newVal)
    {
        Coordinate newCoord = new Coordinate(newVal);

        Node targetNode = GameManager.GetNodeAt(targetPosition).GetNeighbor(newCoord);

        if (targetNode != null)
            targetPosition = Coordinate.ToVector(targetNode.Position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
            WallCollision();

        if (other.tag == "LifeNode")
        {
            lives++;
            Destroy(other.gameObject);
        }
    }

    void WallCollision()
    {
        lives--;

        sphereAnimation.Play();
        gameManager.PlayerCollision();
    }

}
