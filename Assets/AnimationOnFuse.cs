using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


public class AnimationOnFuse : MonoBehaviour
{

    [SerializeField] GameObject animatedPizzaPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] Ease easeType;

    Vector3 targetPosition;
    GameObject pizza;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    void Awake()
    {
        targetPosition = target.position;
       // targetPosition = new Vector3(150f, 200f, -3.27f);
        //  targetPosition = Camera.main.WorldToScreenPoint(target);
        PreparePizza();
    }

    void PreparePizza()
    {
        pizza = Instantiate(animatedPizzaPrefab);
        pizza.transform.parent = transform;
        pizza.SetActive(false);
        Debug.Log("IN Prep");

    }

    public void animate(Vector3 pizzaPlatePosition)
    {

        Debug.Log("piaa" + pizza);
        //  pizza = Instantiate(animatedCoinPrefab);
        Debug.Log("piaa2" + pizza);

        pizza.SetActive(true);

        //move coin to the collected coin pos
        pizza.transform.position = pizzaPlatePosition;

        Debug.Log("pizzaPlate" + pizzaPlatePosition);

        Debug.Log("target" + targetPosition);
        //animate coin to target position
        float duration = Random.Range(minAnimDuration, maxAnimDuration);
        pizza.transform.DOMove(targetPosition, 10f)
        .SetEase(easeType)
  
        .OnComplete(() =>
        {
            Vector3 originalScale = pizza.transform.localScale;
            //executes whenever coin reach target position
            Debug.Log("HERE");
            pizza.SetActive(false);
        });

    }
}
