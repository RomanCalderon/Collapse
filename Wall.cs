using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    WallEngine wallEngine;

    float destroyDelay;
    float speed;
    bool startMovement = false;
    float fadeOutDelay;

    [SerializeField]
    Material wallMaterial;

    [SerializeField]
    GameObject[] tiles = new GameObject[9];

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DestroyTimer());
        StartCoroutine(MoveDelay());
        StartCoroutine(FadeOutStart());

        transform.position += transform.forward * -0.06f;

        StartCoroutine(FadeIn());
	}

    private void Update()
    {
        if (startMovement)
            transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.Self);
    }

    public void Initialize(int tile, float rate, WallEngine wallEngine)
    {
        this.wallEngine = wallEngine;
        wallEngine.gameManager.WallSpawned = true;

        // Increases over time
        speed = 10 / rate;

        destroyDelay = 8f / speed;
        fadeOutDelay = destroyDelay * 0.9f;

        transform.GetChild(tile).gameObject.SetActive(false);
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.5f);
        startMovement = true;
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyDelay);

        wallEngine.gameManager.WallDestroyed = true;
        wallEngine.gameManager.CheckPoint();
        Destroy(gameObject);
    }

    IEnumerator FadeIn()
    {
        float alpha = 0.0f;

        float red = Random.Range(0.0f, 1.0f);
        float green = Random.Range(0.0f, 1.0f);
        float blue = Random.Range(0.0f, 1.0f);
        Color wallColor = new Color(red, green, blue, alpha);

        while (alpha < 0.75f)
        {
            wallColor.a = alpha;
            wallMaterial.color = wallColor;
            alpha += Time.deltaTime * 0.65f;
            yield return null;
        }
    }

    IEnumerator FadeOutStart()
    {
        yield return new WaitForSeconds(fadeOutDelay);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha = 1.0f;

        Color wallColor = wallMaterial.color;

        while (alpha > 0.0f)
        {
            wallColor.a = alpha;
            wallMaterial.color = wallColor;
            alpha -= Time.deltaTime * 1.8f;
            yield return null;
        }
    }
}
