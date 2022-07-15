using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnimationOnFuse : MonoBehaviour
{
    //spawned prefab for animation
    [SerializeField] GameObject animatedPizzaPrefab_allRed;
    [SerializeField] GameObject animatedPizzaPrefab_allYellow;
    [SerializeField] GameObject animatedPizzaPrefab_half;
    [SerializeField] GameObject animatedPizzaPrefab_twoRed;
    [SerializeField] GameObject animatedPizzaPrefab_OneRed;
    [SerializeField] GameObject animatedPizzaPrefab_Star;

    //target location cheatsheet
    [SerializeField] Transform Target_allRed;
    [SerializeField] Transform Target_allYellow;
    [SerializeField] Transform Target_half;
    [SerializeField] Transform Target_twoRed;
    [SerializeField] Transform Target_oneRed;


    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;

    private Vector3 Target_allRed_position;
    private Vector3 Target_allYellow_position;
    private Vector3 Target_half_position;
    private Vector3 Target_twoRed_position;
    private Vector3 Target_oneRed_position;

    private GameObject pizza_allRed;
    private GameObject pizza_allYellow;
    private GameObject pizza_half;
    private GameObject pizza_twoRed;
    private GameObject pizza_oneRed;
    private string level;


    void Awake()
    {
        level = SceneManager.GetActiveScene().name;
        Debug.Log("Level" + level);
        if(level=="Level1")
        {
            Debug.Log("In Animation of" + level);
            Target_allRed_position = Target_allRed.position;
            Target_allYellow_position = Target_allYellow.position;
            Target_half_position = Target_half.position;

            pizza_allRed = Instantiate(animatedPizzaPrefab_allRed);
            pizza_allYellow = Instantiate(animatedPizzaPrefab_allYellow);
            pizza_half = Instantiate(animatedPizzaPrefab_half);

            pizza_allRed.transform.parent = transform;
            pizza_allYellow.transform.parent = transform;
            pizza_half.transform.parent = transform;

            pizza_allRed.SetActive(false);
            pizza_allYellow.SetActive(false);
            pizza_half.SetActive(false);

            Debug.Log("In Preparation Stage : Animation");
        }
        else if (level=="Level2")
        {
            Debug.Log("In Animation of" + level);
            Target_allRed_position = Target_allRed.position;
            Target_allYellow_position = Target_allYellow.position;
            Target_twoRed_position = Target_twoRed.position;

            pizza_allRed = Instantiate(animatedPizzaPrefab_allRed);
            pizza_allYellow = Instantiate(animatedPizzaPrefab_allYellow);
            pizza_twoRed = Instantiate(animatedPizzaPrefab_twoRed);

            pizza_allRed.transform.parent = transform;
            pizza_allYellow.transform.parent = transform;
            pizza_twoRed.transform.parent = transform;

            pizza_allRed.SetActive(false);
            pizza_allYellow.SetActive(false);
            pizza_twoRed.SetActive(false);
        }

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
        Debug.Log("Target Position" + Target_allRed_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Target_allRed_position, pizza_allRed, 3);
    }

    public void animateOnAllYellow(Vector3 sourcePosition)
    {
        Debug.Log("All Yellow pizza" + pizza_allYellow);
        pizza_allYellow.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_allYellow.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Target_allYellow_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Target_allYellow_position, pizza_allYellow,5);
    }
    public void animateOnHalfHalf(Vector3 sourcePosition)
    {
        Debug.Log("Half pizza" + pizza_half);
        pizza_half.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_half.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Target_half_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        if(level=="Level3")
        {
            //for level 3, only two pizza on cheatsheet ,higher jump throw is okay
            doAnimate(Target_half_position, pizza_half, 5);
        } else
        {
            doAnimate(Target_half_position, pizza_half, 2);
        }
    }

    public void animateOnOneRed(Vector3 sourcePosition)
    {
        pizza_oneRed.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_oneRed.transform.position = sourcePosition;
        doAnimate(Target_oneRed_position, pizza_oneRed, 2);
    }


    public void animateOnTwoRed(Vector3 sourcePosition)
    {
        Debug.Log("Two red pizza" + pizza_twoRed);
        pizza_twoRed.SetActive(true);

        //move pizza to the target cheatsheet pos
        pizza_twoRed.transform.position = sourcePosition;

        Debug.Log("Source Position" + sourcePosition);
        Debug.Log("Ease Type" + easeType);
        Debug.Log("Target Position" + Target_twoRed_position);
        //float duration = Random.Range(minAnimDuration, maxAnimDuration);
        doAnimate(Target_twoRed_position, pizza_twoRed, 2);
    }

    void doAnimate(Vector3 target, GameObject gameObject, int jumpPower)
    {
        Vector3 originalScale = gameObject.transform.localScale;
        Sequence s = DOTween.Sequence();
        s.Append(gameObject.transform.DOJump(target, jumpPower, 1, 3.5f, false));
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


