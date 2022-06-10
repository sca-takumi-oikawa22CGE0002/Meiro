using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
    void Start()
    {
        gameobject.GetComponent<Button>().onclick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("MiniGame");
    }
}