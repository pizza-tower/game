using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


public class AnimationOnHalfFuse : MonoBehaviour
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
        //targetPosition = new Vector3(150f, 200f, -3.27f);
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

        pizza.SetActive(true);

        pizza.transform.position = pizzaPlatePosition;

        Debug.Log("pizzaPlate" + pizzaPlatePosition);

        Debug.Log("target" + targetPosition);

        float duration = Random.Range(minAnimDuration, maxAnimDuration);
        pizza.transform.DOMove(targetPosition, 10f)
        .SetEase(easeType)

        .OnComplete(() =>
        {
            Vector3 originalScale = pizza.transform.localScale;
            Debug.Log("HERE");
            pizza.SetActive(false);
        });

    }
}
