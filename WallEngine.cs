using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class WallEngine : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    GameObject wallPrefab;

    [SerializeField]
    bool playing = true;

    Vector3[] locations = new Vector3[6];

    // Use this for initialization
    void Start ()
    {
        gameManager = GetComponent<GameManager>();

        // Set wall spawn locations
        locations[0] = new Vector3(3, 0, 0);
        locations[1] = new Vector3(-3, 0, 0);
        locations[2] = new Vector3(0, 0, 3);
        locations[3] = new Vector3(0, 0, -3);
        locations[4] = new Vector3(0, 3, 0);
        locations[5] = new Vector3(0, -3, 0);

        StartCoroutine(StartGame());

    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(SpawnWall());
    }

    IEnumerator SpawnWall()
    {
        float rate = DifficultyRamp(Time.timeSinceLevelLoad);

        Vector3 pos = locations[Random.Range(0, locations.Length)];

        GameObject wallObj = Instantiate(wallPrefab, pos, GetRotation(pos));

        wallObj.GetComponent<Wall>().Initialize(Random.Range(0, 9), rate, this);

        yield return new WaitForSeconds(rate);

        if(playing)
            StartCoroutine(SpawnWall());
    }

    #region Helper Functions

    private float DifficultyRamp(float x)
    {
        return (-Mathf.Sqrt(x)) / 3 + 6;
    }

    Quaternion GetRotation(Vector3 location)
    {
        return Quaternion.FromToRotation(Vector3.forward, location.normalized);
    }

#endregion
}
