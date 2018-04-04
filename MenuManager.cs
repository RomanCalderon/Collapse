using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    GameObject exitButton;

    [SerializeField]
    float playButtonAppearance = 7f;

    [SerializeField]
    float exitButtonAppearance = 6.85f;

    // Use this for initialization
    void Start ()
    {
        playButton.SetActive(false);
        exitButton.SetActive(false);

        StartCoroutine(PlayButtonDelay());
        StartCoroutine(ExitButtonDelay());
    }
	
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    IEnumerator PlayButtonDelay()
    {
        yield return new WaitForSeconds(playButtonAppearance);
        playButton.SetActive(true);
    }

    IEnumerator ExitButtonDelay()
    {
        yield return new WaitForSeconds(exitButtonAppearance);
        exitButton.SetActive(true);
    }
}
