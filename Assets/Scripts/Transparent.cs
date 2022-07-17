using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    int IsTransparent = 0;
    // Start is called before the first frame update
    List<int> indexesToEdit = new() { 0, 1, 2, 5 };
    void BackToNormal()
    {
        List<List<GameObject>> glist = GlobalData.globalList;
        IsTransparent = 0;
        for(int i = 0; i < indexesToEdit.Count; i++)
        {
            for(int j = 0; j < glist[indexesToEdit[i]].Count; j++)
            {
                glist[indexesToEdit[i]][j].GetComponent<PizzaRotation>().MaterialToNormal();
            }
        }
    }
    void ToTransparent()
    {
        List<List<GameObject>> glist = GlobalData.globalList;
        IsTransparent = 1;
        for (int i = 0; i < indexesToEdit.Count; i++)
        {
            //Do not make transparent if stack height < 4;
            if (glist[i].Count < 4) continue;
            for (int j = 0; j < glist[indexesToEdit[i]].Count; j++)
            {
                glist[indexesToEdit[i]][j].GetComponent<PizzaRotation>().MaterialToTransparent();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(GlobalData.GoTransparent == 1 && IsTransparent == 0)
        {
            ToTransparent();
        }
        if(GlobalData.GoTransparent == 0 && IsTransparent == 1)
        {
            BackToNormal();
        }
    }
}
