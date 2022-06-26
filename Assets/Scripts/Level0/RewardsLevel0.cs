using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsLevel0 : MonoBehaviour
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
    }
    public void LaunchBomb()
    {
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabolaLevel0>().IsBomb = true;
        SliceOnPeel.GetComponent<MaterialsLevel0>().ToBomb();
    }
    public void LaunchColorChanger()
    {
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabolaLevel0>().IsColorChanger = true;
        SliceOnPeel.GetComponent<MaterialsLevel0>().ToRainbow();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
