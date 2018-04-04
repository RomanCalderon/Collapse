using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AudioClip songBassTrack;

    [SerializeField]
    Animation cameraAnimation;

    [SerializeField]
    GameObject lifeNode;
    [SerializeField]
    float nodeSpawnRate = 5f;

    public bool WallSpawned = false;
    public bool PlayerCollided = false;
    public bool WallDestroyed = false;

    int playerScore = 0;

    [SerializeField]
    Text scoreText;

    public static Node[,,] nodes = new Node[3,3,3];

	// Use this for initialization
	void Awake ()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Coordinate pos = new Coordinate(new Vector3(x, y, z));
                    nodes[x, y, z] = new Node(pos);
                }
            }
        }
	}

    private void Start()
    {
        scoreText.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public static Node[,,] GetNodes()
    {
        return nodes;
    }

    public static Node GetNodeAt(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        int z = Mathf.RoundToInt(position.z);

        return nodes[x, y, z];
    }

    public void PlayerCollision()
    {
        PlayerCollided = true;
        print("player does not get a point");

        cameraAnimation.Play();
    }

    public void CheckPoint()
    {
        if(PlayerCollided == false)
        {
            playerScore++;

            scoreText.text = playerScore.ToString();
        }

        WallSpawned = false;
        WallDestroyed = false;
        PlayerCollided = false;
    }
}