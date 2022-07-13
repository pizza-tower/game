using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


public class AnimationOnFuse : MonoBehaviour
{

    [SerializeField] GameObject animatedPizzaPrefab_allRed;
    [SerializeField] GameObject animatedPizzaPrefab_allYellow;
    [SerializeField] GameObject animatedPizzaPrefab_half;
    [SerializeField] GameObject animatedPizzaPrefab_twoRed;
    [SerializeField] GameObject animatedPizzaPrefab_OneRed;
    [SerializeField] GameObject animatedPizzaPrefab_Star;
    [SerializeField] GameObject animatedPizzaPrefab_TwoRed;
    [SerializeField] GameObject animatedPizzaPrefab_TwoYellow;

    [SerializeField] Transform Level1Target_allRed;
    [SerializeField] Transform Level1Target_allYellow;
    [SerializeField] Transform Level1Target_half;

    [SerializeField] Transform Level2Target;
    [SerializeField] Transform Level3Target;
    [SerializeField] Transform Level4Target;
    [SerializeField] Transform Level5Target;


    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;

    private Vector3 Level1Target_allRed_position;
    private Vector3 Level1Target_allYellow_position;
    private Vector3 Level1Target_half_position;

    private GameObject pizza_allRed;
    private GameObject pizza_allYellow;
    private GameObject pizza_half;


    void Awake()
    {
        Level1Target_allRed_position = Level1Target_allRed.position;
        Level1Target_allYellow_position = Level1Target_allYellow.position;
        Level1Target_half_position = Level1Target_half.position;
        PreparePizza();
    }

    void PreparePizza()
    {
        pizza_allRed = Instantiate(animatedPizzaPrefab_allRed);
        pizza_allYellow = Instantiate(animatedPizzaPrefab_allYellow);
        pizza_half= Instantiate(animatedPizzaPrefab_half);

        pizza_allRed.transform.parent = transform;
        pizza_allYellow.transform.parent = transform;
        pizza_half.transform.parent = transform;

        pizza_allRed.SetActive(false);
        pizza_allYellow.SetActive(false);
        pizza_half.SetActive(false);

        Debug.Log("In Preparation Stage : Animation");

    }

    public void animateOnAllRed(Vector3 sourcePosition)
    {
        Debug.Log("All Red pizza" + pizza_allRed);
        pizza_allRed.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_allRed.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Level1Target_allRed_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Level1Target_allRed_position, pizza_allRed);
    }

    public void animateOnAllYellow(Vector3 sourcePosition)
    {
        Debug.Log("All Yellow pizza" + pizza_allYellow);
        pizza_allYellow.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_allYellow.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Level1Target_allYellow_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Level1Target_allYellow_position, pizza_allYellow);
    }
    public void animateOnHalfHalf(Vector3 sourcePosition)
    {
        Debug.Log("Half pizza" + pizza_half);
        pizza_half.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_half.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Level1Target_half_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Level1Target_half_position, pizza_half);
    }

    void doAnimate(Vector3 target, GameObject gameObject)
    {
        Vector3 originalScale = gameObject.transform.localScale;
        Sequence s = DOTween.Sequence();
        s.Append(gameObject.transform.DOJump(target, 5, 1, 3.5f, false));
        s.Join(gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 4f).SetDelay(1.5f));
        s.OnComplete(() => completeAnimation(gameObject, originalScale));
        s.Play();
        Debug.Log("Animation Over");
    }

    void completeAnimation(GameObject gameObject, Vector3 originalScale)
    {
        Debug.Log("Animation on Complete");
        gameObject.SetActive(false);
        gameObject.transform.localScale = originalScale;
    }
}


