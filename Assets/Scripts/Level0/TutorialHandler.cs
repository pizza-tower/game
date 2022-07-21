using System.Net.Security;
using System.Timers;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Net;
using System;
using System.Threading;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    public GameObject Slice;
    bool WalkthroughDone = false;   

    private GameObject ui_handler, ui_flow;
    private int stage = 0;

    private GameObject pizzaSpawner;
    private NewSliceSpawn pizzaSpawnerComponent;
    private float spawnerSpawnTime;

    private GameObject CurrentVertSlice;
    private int slicesCount = 0;
    private bool _pause = false;
    private bool pressSpace = false;
    private bool isPlaced = false;
    private bool blockSpace = false;
    private bool fuseCase = false;
    private bool blockInput = false;

    // components
    private Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> ArrowObjects = new Dictionary<string, GameObject>();


    // Game object names
    class GameObjectNames {
        public static readonly string PIZZA_PEEL = "PizzaPeel";
        public static readonly string PLATE = "Plate";
        public static readonly string SCORE_UI = "ScoreUI";
        public static readonly string LEVEL_UI = "LevelUI";
        public static readonly string GOLD_UI = "GoldUI";
        public static readonly string COLOR_CHANGER = "ColorChanger";
        public static readonly string INSTRUCTION_UI = "InstructionUI";
        public static readonly string CHEAT_SHEET = "CheatSheet";
        public static readonly string FLOW_UI = "FlowUI";
        public static readonly string ANCHORS = "Anchors";
        public static readonly string BUTTON = "Bomb";
        public static readonly string INTRO_UI = "IntroUI";
        public static readonly string SLICE = "Slice";
        public static readonly string SLICE_UI = "SlicesUI";
        public static readonly string REWARDS_UI = "Rewards";
        public static readonly string CHEATS = "Cheats";
    };

    class ArrowObjectNames
    {
        public static readonly string PLATE = "plate_arrow";
        public static readonly string PEEL = "peel_arrow";
        public static readonly string SCORE = "score_arrow";
        public static readonly string HFUSE = "hfuse_arrow";
        public static readonly string GOLD = "gold_arrow";
        public static readonly string CHEAT = "cheat_arrow";
        public static readonly string REWARDS = "rewards_arrow";
    }


    void intro(){
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Lets take a brief Walk through!");
        });
    }

    void DisableAllArrows()
    {
        foreach (KeyValuePair<string, GameObject> key in ArrowObjects)
        {
            ArrowObjects[key.Key].SetActive(false);
        }
    }

    void FuseMessage(int iMessageNumber)
    {

        if (iMessageNumber == 0)
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => {
                x.SetTutorialInstruction("Lets Try Vertical Fuse!\n Try to make stack of three slices with no more than 5 slices!");
            });
        else if (iMessageNumber == 1)
        {
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => {
                x.SetTutorialInstruction("Ops..Try Again! \nTry to stack three slices on same slot");
            });
        }
        else if (iMessageNumber == 2)
        {
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => {
                x.SetTutorialInstruction("Three Slices on the same slot will fuse");
            });
        }
    }

    public int CalculateSlices()
    {
        int size = 0;
        foreach (List<GameObject> obj in GlobalData.globalList)
        {
            size += obj.Count;
        }
        return size;
    }

    void DestroySlices()
    {
        foreach (List<GameObject> obj in GlobalData.globalList)
        {
            foreach (GameObject slice in obj)
            {
                Destroy(slice);
            }
        }
        GlobalData.ResetGlobalList();
    }

    delegate void Function();
    IEnumerator WaitFor(float seconds, Function toCall)
    {
        yield return new WaitForSeconds(seconds);
        toCall();
    }

    void showPeel(){
        DisableAllArrows();
        ArrowObjects[ArrowObjectNames.PEEL].SetActive(true);
        Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);

        Debug.Log("Showing Peel..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Pizza Peel used to throw the pizza on to Pan");
        });
    }

    void showPan(){
        DisableAllArrows();
        Objects[GameObjectNames.PLATE].SetActive(true);
        // ArrowObjects[ArrowObjectNames.PLATE].SetActive(true);
        ArrowObjects[ArrowObjectNames.PLATE].transform.position = new Vector3(-2.66078997f,-4.76942348f,1.46437407f);
        ArrowObjects[ArrowObjectNames.PLATE].transform.rotation = Quaternion.Euler(59.2067375f,180f,180f);
         ArrowObjects[ArrowObjectNames.PLATE].SetActive(true);
        Debug.Log("Showing Pan..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Plate has 6 slots where pizzas can be thrown");
        });
        GlobalData.ValidSlices["Level0"] = new(){ SliceColor.Yellow };
    }
    

    void showPeelAndPizzaSlice()
    {

        if(Objects[GameObjectNames.PIZZA_PEEL].activeSelf == false){
            // GameObject.Find("PizzaPeelPivot").GetComponent<PizzaPeelController>().enabled = false;
            Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);
            
            pizzaSpawner.SetActive(true);
        }else {
            pizzaSpawner.SetActive(true);
            DisableAllArrows();
            ArrowObjects[ArrowObjectNames.PEEL].SetActive(true);

            //Destroy(pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice());

            Debug.Log("Showing Pizza Slice..");

            StartCoroutine(WaitFor(0.5f, () => {
                // ui_flow.SetActive(true);
                if(CurrentVertSlice != null && CurrentVertSlice.activeSelf){
                    Debug.Log("Waiting for Pizza Spawner to be active");
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Pizza Slice rotates until you hit space to throw the pizza");
                    });
                    pressSpace = true;
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetFlowInstruction("(Press Space to throw the Pizza)");
                    });
                    WalkthroughDone = true;
                }

                // For VerticalFuse verification in the next stage `verticalFuseCondition`
                CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
                
            }));
        }

    }

    int instructionCount = 0;
    void verticalFuseCondition(){
        GameObject.Find("PizzaPeelPivot").GetComponent<PizzaPeelController>().enabled = true;

        DisableAllArrows();
        GlobalData.ValidSlices["Level0"] = new(){ SliceColor.Yellow };
        fuseCase = true;
        FuseMessage(instructionCount);
        if(GlobalData.nVerticalFusions > 0)
        {
            DestroySlices();
            Debug.Log("Fusion Over");
            stage++;
            _pause = true;
            CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
            ArrowObjects[ArrowObjectNames.HFUSE].SetActive(false);
            return;
        }

        ArrowObjects[ArrowObjectNames.HFUSE].SetActive(true);

        Debug.Log("CurrentVertSlice Name: " + (CurrentVertSlice != null ? CurrentVertSlice.name : "null"));
        if(CurrentVertSlice != null){
            
            isPlaced = CurrentVertSlice.GetComponent<PizzaParabola>().GetIsPlaced();
            if(!isPlaced)
                return;
            
            isPlaced = false;
            Debug.Log("Slices Count: " + CalculateSlices());
            Debug.Log("GlobalData.isFirstFusionOver: " + GlobalData.isFirstFusionOver);
            if(instructionCount == 1 && CalculateSlices() == 1)
                instructionCount = 2;

            if(CalculateSlices() > 5){
                instructionCount = 1;
                DestroySlices();
                slicesCount = 0;
            }
        }
        CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
    }

    void HFuseMessage(int i){
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetFlowInstruction("(Press Space to throw the Pizza)");
        });
        
        if(i == 0){
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Lets Try Horizontal Fuse!\n Try to throw the pizza in the empty slot");
            });
        }else if(i == 1){
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Oops..Try Again!\n Try to throw the pizza in the empty slot");
            });
        }
        
    }

    bool isHInitDone = false;
    int instr = 0;
    void horizontalFuse(){
        GlobalData.ValidSlices["Level0"] = new() { SliceColor.Red };
        if (CurrentVertSlice == null || CurrentVertSlice.activeSelf == false)
        {
            CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
            isHInitDone = true;
        }

        Debug.Log("Slice: " + CurrentVertSlice != null ? CurrentVertSlice.name: "null");
        fuseCase = true;
        
        HFuseMessage(instr);
        
        Objects[GameObjectNames.INTRO_UI].SetActive(false);
        Objects[GameObjectNames.PLATE].SetActive(true);
        Objects[GameObjectNames.INSTRUCTION_UI].SetActive(true);
        Objects[GameObjectNames.FLOW_UI].SetActive(true);
        Objects[GameObjectNames.ANCHORS].SetActive(true);
        Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);
        pizzaSpawner.SetActive(true);

        //Debug.Log("GlobalData.nHorizontalFusions: " + GlobalData.nHorizontalFusions);
        if (GlobalData.nHorizontalFusions > 0) { 
            stage++;
            _pause = true;
            pressSpace = false;
            fuseCase = false;
            return;
        }
        TutorialHFuse.checkGlobalList(Slice);
        //CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();

        if (CurrentVertSlice != null){

            isPlaced = CurrentVertSlice.GetComponent<PizzaParabola>().GetIsPlaced();
            if (isPlaced)
            {
                //isPlaced = false;
                Debug.Log("HFUSE CurrentVertSlice Name: " + CurrentVertSlice.name);
                Debug.Log("GlobalData.nHorizontalFusions: " + GlobalData.nHorizontalFusions);
                if (GlobalData.nHorizontalFusions == 0)
                {
                    //isHInitDone = false;
                    instr = 0;
                    ArrowObjects[ArrowObjectNames.HFUSE].SetActive(true);
                }
                
            }
        }
        
        Debug.Log("CurrentVertSlice Name: " + (CurrentVertSlice != null ? CurrentVertSlice.name : "null"));
        //}
    }

    private IEnumerator Walkthrough()
    {

        // Debug.Log("Slices Count: " + CalculateSlices() + " -> " + stage);

        while (!_pause){
            yield return new WaitForSeconds(0.5f);

            if(stage == 1){
                if(WalkthroughDone == false){
                    WalkthroughDone = true;
                    GameObject.Find("IntroUI").SetActive(false);

                    Objects[GameObjectNames.PLATE].SetActive(true);
                    Objects[GameObjectNames.INSTRUCTION_UI].SetActive(true);
                    Objects[GameObjectNames.FLOW_UI].SetActive(true);
                    Objects[GameObjectNames.ANCHORS].SetActive(true);
                    showPan();_pause = true;
                }
            }

            //if(stage == 2){
            //    if(WalkthroughDone == false){
            //        WalkthroughDone = true;
                    
            //        Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);
            //        showPeel();_pause = true;
            //    }
            //}
            if(stage == 2)stage++;

             //TODO : Peel and slice animation not synchronized
            if (stage == 3){
                if(WalkthroughDone == false){
                    showPeelAndPizzaSlice();_pause = true;
                }

                // For next stage
                pressSpace = true;
                // DestroySlices();
            }

            if(stage == 4)
            {
                GameObject NewSlice = Instantiate(Slice) as GameObject;
                NewSlice.tag = "hardcoded";
                NewSlice.name = "colv1_slice1";
                NewSlice.transform.position = new Vector3(4.36000013f, -1.04999995f, -10.8999996f);
                NewSlice.transform.rotation = Quaternion.Euler(new Vector3(0f, 300f, 0f));

                NewSlice.GetComponent<PizzaRotation>().mColor = SliceColor.Yellow;
                NewSlice.GetComponent<PizzaRotation>().hardcoded = true;
                NewSlice.GetComponent<PizzaParabola>().enabled = false;

                GlobalData.globalList[5].Add(NewSlice);

                NewSlice = Instantiate(Slice) as GameObject;
                NewSlice.tag = "hardcoded";
                NewSlice.name = "colv1_slice2";
                NewSlice.transform.position = new Vector3(4.36000013f, -0.850000024f, -10.8999996f);
                NewSlice.transform.rotation = Quaternion.Euler(new Vector3(0f, 300f, 0f));

                NewSlice.GetComponent<PizzaRotation>().mColor = SliceColor.Yellow;
                NewSlice.GetComponent<PizzaRotation>().hardcoded = true;
                NewSlice.GetComponent<PizzaParabola>().enabled = false;

                GlobalData.globalList[5].Add(NewSlice);

                stage++;
            }
            if(stage == 5){
                verticalFuseCondition();
            }
            
            // TODO : Add Horizontal Fuse

            if(stage == 6){
                _pause = false;
                horizontalFuse();
            }

            if(stage == 7){
                DisableAllArrows();
                pressSpace = false;
                pizzaSpawner.SetActive(false);
                //Objects[GameObjectNames.CHEAT_SHEET].SetActive(true);
                Objects[GameObjectNames.PIZZA_PEEL].SetActive(false);
                Objects[GameObjectNames.PLATE].SetActive(false);
                DestroySlices();
                Destroy(pizzaSpawnerComponent.GetSpawnedSlice());
                Objects[GameObjectNames.CHEATS].SetActive(true);
                Objects[GameObjectNames.ANCHORS].SetActive(false);
                Objects[GameObjectNames.INSTRUCTION_UI].SetActive(false);

                Objects[GameObjectNames.INTRO_UI].SetActive(true);

                ArrowObjects[ArrowObjectNames.CHEAT].SetActive(true);
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("NOTE: Ways to make Horizontal Fuses");
                });
                _pause = true;
            }

            // TODO : Add Animation/Video to better understand Power Ups
            if(stage == 8){
                //Objects[GameObjectNames.SCORE_UI].SetActive(true);
                DisableAllArrows();
                ArrowObjects[ArrowObjectNames.SCORE].SetActive(true);
                Objects[GameObjectNames.SLICE_UI].SetActive(true);

                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("These are the Number of SLICES available");
                });
                _pause = true;
            }

            if(stage == 9){
                DisableAllArrows();
                ArrowObjects[ArrowObjectNames.REWARDS].SetActive(true);


                //Objects[GameObjectNames.GOLD_UI].SetActive(true);
                Objects[GameObjectNames.BUTTON].SetActive(true);
                Objects[GameObjectNames.COLOR_CHANGER].SetActive(true);
                Objects[GameObjectNames.REWARDS_UI].SetActive(true);

                Objects[GameObjectNames.CHEATS].SetActive(true);
                Objects[GameObjectNames.SLICE_UI].SetActive(true);

                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("");
                });
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => {
                    x.SetFlowInstruction("");
                });
                _pause = true;
            }

            // if(stage == 9){
            //     // Objects[GameObjectNames.PIZZA_PEEL].SetActive(false);
            //     DisableAllArrows();
            //     ArrowObjects[ArrowObjectNames.SCORE].SetActive(true);

            //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            //         x.setIntroInstruction("AIM: Gain the total score needed to complete the level");
            //     });
            //     _pause = true;
            // }

            if(stage == 10){

                foreach (KeyValuePair<string, GameObject> key in Objects)
                {
                    Objects[key.Key].SetActive(false);
                }

                Objects.Add(GameObjectNames.INTRO_UI, GameObject.Find(GameObjectNames.INTRO_UI));
                Objects.Add(GameObjectNames.FLOW_UI, GameObject.Find(GameObjectNames.FLOW_UI));
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("Cool Right!\n Press Enter to Start Level1");
                });
                _pause = true;
            }

            if(stage == 11){
                SceneManager.LoadScene(3);
            }
        }
    }


    void Start(){
        // Initialization
        Objects.Add(GameObjectNames.PIZZA_PEEL, GameObject.Find(GameObjectNames.PIZZA_PEEL));
        Objects.Add(GameObjectNames.PLATE, GameObject.Find(GameObjectNames.PLATE));
        //Objects.Add(GameObjectNames.SCORE_UI, GameObject.Find(GameObjectNames.SCORE_UI));
        Objects.Add(GameObjectNames.LEVEL_UI, GameObject.Find(GameObjectNames.LEVEL_UI));
        //Objects.Add(GameObjectNames.GOLD_UI, GameObject.Find(GameObjectNames.GOLD_UI));
        Objects.Add(GameObjectNames.COLOR_CHANGER, GameObject.Find(GameObjectNames.COLOR_CHANGER));
        Objects.Add(GameObjectNames.INSTRUCTION_UI, GameObject.Find(GameObjectNames.INSTRUCTION_UI));
        //Objects.Add(GameObjectNames.CHEAT_SHEET, GameObject.Find(GameObjectNames.CHEAT_SHEET));
        Objects.Add(GameObjectNames.FLOW_UI, GameObject.Find(GameObjectNames.FLOW_UI));
        Objects.Add(GameObjectNames.ANCHORS, GameObject.Find(GameObjectNames.ANCHORS));
        Objects.Add(GameObjectNames.BUTTON, GameObject.Find(GameObjectNames.BUTTON));
        Objects.Add(GameObjectNames.INTRO_UI, GameObject.Find(GameObjectNames.INTRO_UI));
        Objects.Add(GameObjectNames.SLICE, GameObject.Find(GameObjectNames.SLICE));
        Objects.Add(GameObjectNames.REWARDS_UI, GameObject.Find(GameObjectNames.REWARDS_UI));
        Objects.Add(GameObjectNames.CHEATS, GameObject.Find(GameObjectNames.CHEATS));
        Objects.Add(GameObjectNames.SLICE_UI, GameObject.Find(GameObjectNames.SLICE_UI));

        ArrowObjects.Add(ArrowObjectNames.PLATE, GameObject.Find(ArrowObjectNames.PLATE));
        ArrowObjects.Add(ArrowObjectNames.PEEL, GameObject.Find(ArrowObjectNames.PEEL));
        ArrowObjects.Add(ArrowObjectNames.SCORE, GameObject.Find(ArrowObjectNames.SCORE));
        ArrowObjects.Add(ArrowObjectNames.HFUSE, GameObject.Find(ArrowObjectNames.HFUSE));
        //ArrowObjects.Add(ArrowObjectNames.GOLD, GameObject.Find(ArrowObjectNames.GOLD));
        ArrowObjects.Add(ArrowObjectNames.CHEAT, GameObject.Find(ArrowObjectNames.CHEAT));
        // ArrowObjects.Add(ArrowObjectNames.REWARDS, GameObject.Find(ArrowObjectNames.REWARDS));

        foreach(KeyValuePair<string, GameObject> key in Objects){
            Objects[key.Key].SetActive(false);
        }
        
        Objects[GameObjectNames.INTRO_UI].SetActive(true);
        ui_handler = GameObject.Find("UIHandler");
        ui_flow = GameObject.FindWithTag("flowui");
        
        pizzaSpawner = GameObject.FindWithTag("Spawner");
        pizzaSpawnerComponent = pizzaSpawner.GetComponent<NewSliceSpawn>();
        pizzaSpawner.SetActive(false);

        DestroySlices();
        DisableAllArrows();
        //
        TutorialHFuse.InitializeVars();
    }

    void Update()
    {   
        // Debug.Log("stage: "+ stage + " ----->" + fuseCase + " ----->" + WalkthroughDone);
        StartCoroutine(Walkthrough());
        if (((!pressSpace && Input.GetKeyDown(KeyCode.Return)) 
            || (pressSpace &&  Input.GetKeyDown(KeyCode.Space)) && !fuseCase)){
            
            if(!fuseCase){
                WalkthroughDone = false;
                stage++;
            }
            _pause = false;
        }
    }
}
