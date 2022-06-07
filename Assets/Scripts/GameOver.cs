using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text pointsText;
    public void setup(int score){
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton(){
        SceneManager.LoadScene("Game");
    }

    public void ExitButton(){
        SceneManager.LoadScene("MainMenu");
    }
}

// In the controller.cs file create an object of GameOver 
// Create a function GameOver(){GameOver.setup(points)}
// On the Unity Engine Drag and Drop the components in settings to connect different scripts. 