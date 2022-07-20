using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject SliceOnPeel;
    public GameObject ColorPickerUI;
    void Start()
    {
        
    }
    public static void EarnCurrency()
    {
        //EnableDisableButtons();
        //ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(1));
        GameObject ui_handler = GameObject.Find("UIHandler");
        Vector3 pos;
        pos.x = 0;
        pos.y = 1;
        pos.z = 0;
        //ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.ShowPopupText("Gold+1 !", pos));
    }
    public void LaunchBomb()
    {
        GlobalData.LevelRewardConsume++;
        Score.numPowersUsed++;
        //ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(-1));
        SliceOnPeel = GameObject.FindWithTag("NS");
        SliceOnPeel.GetComponent<PizzaParabola>().IsBomb = true;
        SliceOnPeel.GetComponent<Materials>().ToBomb();
        GameObject.Find("Bomb").GetComponent<Button>().interactable=false;
    }
    public void LaunchColorChanger()
    {
        /*if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        EnableDisableButtons();
        GlobalData.LevelRewardConsume++;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(-1));
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabola>().IsColorChanger = true;
        SliceOnPeel.GetComponent<Materials>().ToRainbow();*/

        ColorPickerUI.SetActive(!ColorPickerUI.activeSelf);
    }

    /*
    private static void EnableDisableButtons()
    {
        Button colorChangerButton = GameObject.Find("ColorChanger").GetComponent<Button>();
        Button bombButton = GameObject.Find("Button").GetComponent<Button>();
        if(RewardsCurrency<=0)
        {
            colorChangerButton.interactable=false;
            bombButton.interactable=false;
        }
        else
        {
            colorChangerButton.interactable=true;
            bombButton.interactable=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
