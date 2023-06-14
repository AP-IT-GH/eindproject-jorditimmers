using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoPauzeMenu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Pause"); // Load the specified scene
    }
}
