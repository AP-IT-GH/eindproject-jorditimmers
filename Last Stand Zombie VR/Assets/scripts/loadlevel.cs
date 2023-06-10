using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour
{
  

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene"); // Load the specified scene
    }
}
