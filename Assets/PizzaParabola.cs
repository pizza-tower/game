using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;
using UnityEngine.Analytics;

public class PizzaParabola : MonoBehaviour
{
    private int Level;
    // Start is called before the first frame update
    protected float Animation = 0;
    Vector3 StartPoint;
    Vector3 EndPoint;
    Vector3 StartRotation;
    Vector3 EndRotation;
    private float IsThrowing = 0;
    private int TargetList = 0;
    private string TargetAnchor = "";
    private GameObject AnchorToFind;
    private SliceList List;
    bool IsPlaced = false;
    public bool IsBomb = false;
    public bool IsColorChanger = false;
    private int fusionIndex = -1;
    AnimationOnFuse Animator;

    float FreeFallTime = 0f;
    bool RandomDropSliceAddedToTheList = false;
    bool SliceAddedToTheList = false;
    bool RequestedNewRandomDropSlice = false;

    void Start()
    {
        if(GetComponent<PizzaRotation>().IsRandomDrop)
        {
            StartPoint = transform.position;
            FreeFallSlice();
        }
      
    }
    // Update is called once per frame
    void AssignTarget()
    {
        switch(GetComponent<PizzaRotation>().TagInInt)
        {
            case 0:
                TargetList = 0;
                TargetAnchor = "AnchorOne";
                break;
            case 1:
                TargetList = 1;
                TargetAnchor = "AnchorTwo";
                break;
            case 2:
                TargetList = 2;
                TargetAnchor = "AnchorThree";
                break;
            case 3:
                TargetList = 3;
                TargetAnchor = "AnchorFour";
                break;
            case 4:
                TargetList = 4;
                TargetAnchor = "AnchorFive";
                break;
            case 5:
                TargetList = 5;
                TargetAnchor = "AnchorSix";
                break;
        }
    }
    void AddToList()
    {
        //only add the gameobject to the list when it is a slice
        if(IsBomb == false && IsColorChanger == false)
        {
            GlobalData.globalList[TargetList].Add(gameObject);
        }    
    }

    void Bomb()
    {
        IsBomb = false;
        FuseSlice.BombFuse(GlobalData.globalList[TargetList]);
        
        Destroy(gameObject);
    }
    void ChangeColor()
    {
        IsColorChanger = false;
        if(GlobalData.globalList[TargetList].Count >= 1)
        {
            GlobalData.globalList[TargetList][GlobalData.globalList[TargetList].Count - 1].GetComponent<Materials>().FlipColor();
        }
        //Check if changing color caused a fusion
        FusionCheck();
        
        Destroy(gameObject);
    }
    void StopRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 1;
    }
    void FreeFallSlice()
    {
        AssignTarget();
        EndPoint = (GameObject.FindWithTag(TargetAnchor)).transform.position;
        float Count = GlobalData.globalList[TargetList].Count + 1;
        EndPoint.y += 0.2f * Count;
    }
    void ThrowSlice()
    {
        AssignTarget();
        EndPoint = (GameObject.FindWithTag(TargetAnchor)).transform.position;
        EndRotation.y += (float)GetComponent<PizzaRotation>().TagInInt * (float)60.0;
        float Count = GlobalData.globalList[TargetList].Count + 1;
        EndPoint.y += 0.2f * Count;

        //Pizza slice throw audio effect
        if(GetComponent<PizzaRotation>().IsRandomDrop == false)
        {
            AudioSource audioData;
            audioData = GameObject.Find("PizzaPeel").GetComponent<AudioSource>();
            audioData.Play(0);

            Debug.Log($"Position: {EndPoint}");    
        }
        
    }
    void Update()
    {
        if(IsPlaced)
        {
            return;
        }
        //if it is a random drop slice, escape the parabola part
        if(GetComponent<PizzaRotation>().IsRandomDrop)
        {
            transform.position = Vector3.Lerp(StartPoint, EndPoint, FreeFallTime/7.0f);
            FreeFallTime += Time.deltaTime;
            //ONLY ADD to the list when the slice is super close to the plate
            //At the begining ask for the next random spawn slice
            if(FreeFallTime/7.0f > 0.1f && RequestedNewRandomDropSlice == false)
            {
                RequestedNewRandomDropSlice = true;
                ((GameObject.FindWithTag("RandomSliceSpawner")).GetComponent<RandomDropSpawn>()).NeedsNewSlice = 1;
            }
            //When the random drop slice is close to the stack, add it to the list
            if(FreeFallTime/7.0f > 0.98f && RandomDropSliceAddedToTheList == false)
            {
                RandomDropSliceAddedToTheList = true;
                ThrowSlice();
                AddToList();
            }
            //when the random drop slice is landed on top of the stack, mark it as ISPlaced
            if(FreeFallTime >= 7.0f)
            {
                FreeFallTime = 7.0f;
                IsPlaced = true;
                FuseSlice.mHorizontalFuse();
                FuseSlice.mVertFuse(GlobalData.globalList[TargetList]);
            }
            return;
        }
        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        //only throw the slice space key is pressed down and there is no slice in the air
        if (KeyDown == true && KeyHold == true && KeyUp == false && IsThrowing == 0)
        {
            Debug.Log("Space is pressed");
            //StartRotation = transform.rotation;
            ThrowSlice();
            StartPoint = transform.position;
            StopRotation();
            IsThrowing = 1;
            //request new slice from the slice spawner, and reset the peel animation
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
            ((GameObject.FindWithTag("Peel")).GetComponent<PizzaPeelController>()).Reset();
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            //when the slice is super close to the stack, add it to the list
            if(Animation/1.3f > 0.98f && SliceAddedToTheList == false)
            {
                SliceAddedToTheList = true;
                //update the land position again
                EndPoint = (GameObject.FindWithTag(TargetAnchor)).transform.position;
                float Count = GlobalData.globalList[TargetList].Count + 1;
                EndPoint.y += 0.2f * Count;
                AddToList();
            }
            //when the slice is landed on top of the stack, mark it as ISPLACED
            if(Animation >= 1.3f)
            {
                Animation = 1.3f;
                IsThrowing = 0;
                IsPlaced = true;
                //Refresh the spawner and generate a new slice
                if(IsBomb)
                {
                    Bomb();
                    return;
                }
                //If it is a color changer, change the color
                if(IsColorChanger)
                {
                    ChangeColor();
                    return;
                }
                //once the throw animation is completed, check the fuse
                FusionCheck();
                
                
                if (GlobalData.globalList[TargetList].Count >= 6) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startWobble();
                }
                if (GlobalData.globalList[TargetList].Count >= 9) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 1.3f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(EndRotation), 6 * Time.deltaTime);
            animateOnFuse(EndPoint);
        }
        

    }

    void animateOnFuse(Vector3 sliceLandingPoint)
    {
        Debug.Log("Fusion Index: " + fusionIndex);
        Animator = FindObjectOfType<AnimationOnFuse>();
        Debug.Log(Animator);
        if (GlobalData.isHorizontalFuse==true && fusionIndex!=-1)
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            Animator = FindObjectOfType<AnimationOnFuse>();
            switch (currentLevel)
            {
                case "Level1":
                    triggerLevelOneAnimation();
                    break;
                case "Level2":
                    triggerLevelTwoAnimation();
                    break;
                case "Level3":
                    triggerLevelThreeAnimation();
                    break;
                case "Level4":
                    triggerLevelFourAnimation();
                    break;
                case "Level5":
                    triggerLevelFiveAnimation();
                    break;
            }

            GlobalData.isHorizontalFuse = false;
        }
    }

    void triggerLevelOneAnimation()
    {
        Vector3 start = transform.position;
        start.y = transform.position.y + 9 * 0.2f;
        switch (fusionIndex)
        {
            case 0:
                Animator.animateOnAllRed(start);
                break;
            case 1:
                Animator.animateOnAllYellow(start);
                break;
            case 2:
                Animator.animateOnHalfHalf(start);
                break;
        }
    }

    void triggerLevelTwoAnimation()
    {
        Vector3 start = transform.position;
        start.y = transform.position.y + 9 * 0.2f;
        switch (fusionIndex)
        {
            case 0:
                Animator.animateOnTwoRed(start);
                break;
            case 1:
                Animator.animateOnAllYellow(start);
                break;
            case 2:
                Animator.animateOnAllRed(start);
                break;
        }
    }

    void triggerLevelFiveAnimation()
    {
        Vector3 start = transform.position;
        start.y = transform.position.y + 9 * 0.2f;
        switch (fusionIndex)
        {
            case 0:
                Animator.animateOnAllRed(start);
                break;
            case 1:
                Animator.animateOnAllYellow(start);
                break;
            case 2:
                Animator.animateOnTwoRed(start);
                break;
        }
    }
    void triggerLevelThreeAnimation()
    {
        Vector3 start = transform.position;
        start.y = transform.position.y + 9 * 0.2f;
        switch (fusionIndex)
        {
            case 0:
                Animator.animateOnAllRed(start);
                break;
            case 1:
                Animator.animateOnAllYellow(start);
                break;
            case 2:
                Animator.animateOnTwoRed(start);
                break;
        }
    }
    void triggerLevelFourAnimation()
    {
        Vector3 start = transform.position;
        start.y = transform.position.y + 9 * 0.2f;
        switch (fusionIndex)
        {
            case 0:
                Animator.animateOnAllRed(start);
                break;
            case 1:
                Animator.animateOnAllYellow(start);
                break;
            case 2:
                Animator.animateOnTwoRed(start);
                break;
        }
    }

    void FusionCheck()
    {
        FuseSlice.mVertFuse(GlobalData.globalList[TargetList]);
        int verticalFuseHappened = 1;
        while (verticalFuseHappened == 1)
        {
            verticalFuseHappened = 0;
            int r;
            fusionIndex= FuseSlice.mHorizontalFuse();
            if(fusionIndex != -1)
            {
                for (int i = 0; i < 6; i++)
                {
                    r = FuseSlice.mVertFuse(GlobalData.globalList[i]);
                    if (r != 0)
                    {
                        verticalFuseHappened = 1;
                    }
                }
            }
        }
    }

}