using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IPizzaTowerUIMessageTarget : IEventSystemHandler
{
    // functions that can be called via the messaging system
    void SetScore(int s);
    void SetScoreRequired(int s);
    void IncrementScore(int s);
    void SetTutorialInstruction(string text);
    void SetLevel(int l);
    void SetGold(int g);
    void IncrementGold(int g);
    void SetFlowInstruction(string text);
    void setIntroInstruction(string text);

    public void ShowPopupText(string text, Vector3 position);
    //Popup text functions

}

public class UIHandlerScript : MonoBehaviour, IPizzaTowerUIMessageTarget
{

    public Text scoreText;
    public Text levelText;
    public Text goldText;
    public Text slicesText;
    public GameObject floatingTextPrefab;
    public Text Instruction;
    public Text FlowInstruction;
    public Text IntroInstruction;

    private int score = 0;
    private int score_required = 1;
    private int gold = 2;

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + score.ToString() + "/" +score_required.ToString();
    }
    private void UpdateLevelText()
    {
        levelText.text = "Level\n" + GlobalData.level.ToString();
    }
    private void UpdateGoldText()
    {
        goldText.text = "Gold\n" + gold.ToString();
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

    public void SetFlowInstruction(string inst){
        FlowInstruction.text = inst;
    }

    public void setIntroInstruction(string inst){
        IntroInstruction.text = inst;
    }
    
    public void ShowPopupText(string text, Vector3 position)
    {
        GameObject prefab = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        prefab.transform.Rotate(30f, 40f, 0f);
        prefab.GetComponentInChildren<TextMesh>().text = text;
        Destroy(prefab, 1f);
    }
    public void SetGold(int g)
    {
        gold = g;
        UpdateGoldText();
    }
    public void IncrementGold(int g)
    {
        gold += g;
        UpdateGoldText();
    }

    private void Start()
    {
        int m = GlobalData.MaxSlices[SceneManager.GetActiveScene().name];
        slicesText.text = "Slices Remaining\n" + m.ToString();
    }

    private void Update()
    {
        int m = GlobalData.MaxSlices[SceneManager.GetActiveScene().name];
        GameObject spawner = GameObject.Find("PizzaSpawner");
        if(spawner != null && spawner.activeSelf) {
            int c = spawner.GetComponent<NewSliceSpawn>().NumberSpawned;
            int v = m - c;

            slicesText.text = "Slices Remaining\n" + v.ToString(); 
        }
    }
}