using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);//load lai Scene 0
        Destroy(gameObject); //destroy GameSession luon
    }
    public void PlayAgain()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        //load lai scene hien tai
        SceneManager.LoadScene(currentsceneindex);
    }
}
