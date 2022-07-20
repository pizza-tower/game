using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectColorUI : MonoBehaviour
{
    public GameObject prefab;
    public GameObject ColorPickerUI;
    // Start is called before the first frame update
    void Start()
    {
        List<SliceColor> slist = GlobalData.ValidSlices[SceneManager.GetActiveScene().name];
        GetComponent<RectTransform>().sizeDelta = new Vector2(slist.Count * 60 + 10, 100);
        for (int i = 0; i < slist.Count; i++)
        {
            AddColor(slist[i], 60*i + 10);
        }
    }

    void AddColor(SliceColor c, int offset)
    {
        
        GameObject button = (GameObject)Instantiate(prefab);
        button.GetComponent<RectTransform>().SetParent(transform);
        button.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        button.GetComponent<RectTransform>().transform.localPosition = new Vector3(offset, -50, 0);
        // button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, 50);

        button.GetComponent<Image>().color = SliceColorToRGB(c);
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Rewards.RewardsCurrency < 1)
            {
                ColorPickerUI.SetActive(false);
                return;
            }
            Rewards.RewardsCurrency--;
            GlobalData.LevelRewardConsume++;
            Score.numPowersUsed++;
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(GameObject.Find("UIHandler"), null, (x, y) => x.IncrementGold(-1));
            GameObject SliceOnPeel = GameObject.FindWithTag("NS");
            //SliceOnPeel.GetComponent<PizzaParabola>().IsColorChanger = true;
            ChangeSliceColor(SliceOnPeel, c);
            ColorPickerUI.SetActive(false);
        });
    }

    Color SliceColorToRGB(SliceColor c)
    {
        switch (c)
        {
            case SliceColor.Red:
                return new Color(1.0f, 0f, 0f);
            case SliceColor.Green:
                return new Color(0.0f, 1.0f, 0.0f);
            case SliceColor.Blue:
                return new Color(0.0f, 0.0f, 1.0f);
            case SliceColor.Yellow:
                return new Color(1.0f, 1.0f, 0.0f);
            case SliceColor.DarkBrown:
                return new Color(0.2f, 0.03f, 0.0f);

        }
        return new Color(0.1f, 0.1f, 0.1f);
    }

    void ChangeSliceColor(GameObject g, SliceColor c)
    {
        g.GetComponent<PizzaRotation>().mColor = c;
        var t = g.GetComponent<Materials>();
        switch (c)
        {
            case SliceColor.Red:
                t.ToRed();
                break;
            case SliceColor.Green:
                t.ToGreen();
                break;
            case SliceColor.Blue:
                t.ToBlue();
                break;
            case SliceColor.Yellow:
                t.ToYellow();
                break;
            case SliceColor.DarkBrown:
                t.ToDarkBrown();
                break;
        }
    }
}
