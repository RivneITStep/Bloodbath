using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject ExitButton;
    void Start()
    {
        PlayButton.GetComponent<Button>().onClick.AddListener(delegate { PlayOnClick(); });
        ExitButton.GetComponent<Button>().onClick.AddListener(delegate { ExitOnClick(); });


    }

    // Update is called once per frame
    void Update()
    {

    }



    void PlayOnClick()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    void ExitOnClick()
    {
        Application.Quit(0);
    }
}
