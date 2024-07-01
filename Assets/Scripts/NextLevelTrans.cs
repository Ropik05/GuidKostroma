using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrans : MonoBehaviour
{
     public void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void changeSceneBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
<<<<<<< Updated upstream:Assets/Scripts/NextLevelTrans.cs
=======

    public void changeSceneBackMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void changeSceneMenu()
    {
        SceneManager.LoadScene(0);
    }
>>>>>>> Stashed changes:Assets/Scripts/NextLevelTrans1.cs
}
