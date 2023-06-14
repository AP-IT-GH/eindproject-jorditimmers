using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Main Menu"); // Load the specified scene
    }
}
