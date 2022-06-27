using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rewards : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject SliceOnPeel;
    public static int RewardsCurrency = 2; 
    void Start()
    {
        
    }
    public static void EarnCurrency()
    {
        RewardsCurrency += 1;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(1));
        GameObject ui_handler = GameObject.Find("UIHandler");
        Vector3 pos;
        pos.x = 0;
        pos.y = 1;
        pos.z = 0;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.ShowPopupText("Gold+1 !", pos));
    }
    public void LaunchBomb()
    {
        //Debug.Log($"Rewards Currency {RewardsCurrency}");
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(-1));
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabola>().IsBomb = true;
        SliceOnPeel.GetComponent<Materials>().ToBomb();
    }
    public void LaunchColorChanger()
    {
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(-1));
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabola>().IsColorChanger = true;
        SliceOnPeel.GetComponent<Materials>().ToRainbow();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
