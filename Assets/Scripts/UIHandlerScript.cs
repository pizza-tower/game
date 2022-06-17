using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IPizzaTowerUIMessageTarget : IEventSystemHandler
{
    // functions that can be called via the messaging system
    void SetScore(int s);
    void SetScoreRequired(int s);
    void IncrementScore(int s);
    void SetTutorialInstruction(string text);
    void SetLevel(int l);
    public void ShowPopupText(string text, Vector3 position);

    //Popup text functions

}

public class UIHandlerScript : MonoBehaviour, IPizzaTowerUIMessageTarget
{

    public Text scoreText;
    public Text levelText;
    public GameObject floatingTextPrefab;
    public Text Instruction;

    private int score = 0;
    private int score_required = 1;

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + score.ToString() + "/" +score_required.ToString();
    }
    private void UpdateLevelText()
    {
        levelText.text = "Level\n" + GlobalData.level.ToString();
    }

    public void SetScore(int s)
    {
        score = s;
        UpdateScoreText();
    }
    public void SetScoreRequired(int s)
    {
        score_required = s;
        UpdateScoreText();
    }

    public void IncrementScore(int s)
    {
        score += s;
        UpdateScoreText();
    }

    public void SetLevel(int l)
    {
        GlobalData.level = l;
        UpdateLevelText();
    }
    public void SetTutorialInstruction(string instruction)
    {
        Instruction.text = instruction;
    }
    public void ShowPopupText(string text, Vector3 position)
    {
        GameObject prefab = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        prefab.transform.Rotate(30f, 40f, 0f);
        prefab.GetComponentInChildren<TextMesh>().text = text;
        Destroy(prefab, 1f);
    }
}