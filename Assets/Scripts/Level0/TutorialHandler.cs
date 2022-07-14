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
using UnityEngine. SceneManagement;

public class TutorialHandler : MonoBehaviour
{

    bool WalkthroughDone = false;   

    private GameObject ui_handler,ui_flow;
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
        public static readonly string BUTTON = "Button";
        public static readonly string INTRO_UI = "IntroUI";
    };


    void intro(){
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Lets take a brief Walk through!");
        });
    }

    void showPeel(){
        Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);
        Debug.Log("Showing Peel..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Pizza Peel used to throw the pizza on to Pan");
        });
    }

    void showPan(){
        GameObject.Find("Plate").SetActive(true);
        Debug.Log("Showing Pan..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Plate has 6 slots where pizzas can be thrown");
        });
        GlobalData.ValidSlices["Level0"] = new(){ SliceColor.Yellow };
    }
    
    delegate void Function();
    IEnumerator WaitFor(float seconds, Function toCall){
            yield return new WaitForSeconds(seconds);
            toCall();
    }

    void showPizzaSlice(){

        pizzaSpawner.SetActive(true);
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
            if(CurrentVertSlice == null)
                Debug.Log("CurrentVertSlice is null");
            else
                Debug.Log("CurrentVertSlice is not null");

            Debug.Log("CurrentVertSlice: " + CurrentVertSlice.name);
        }));
    }

    void FuseMessage(int iMessageNumber){
        if(iMessageNumber == 0)
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Lets Try Vertical Fuse!\n Try to make stack of three slices with no more than 5 slices!");
            });

        if(iMessageNumber == 1){
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Ops..Try Again! \nTry to stack three slices on same slot");
            });
            instructionCount  = 2;
        }

        if(iMessageNumber == 2){
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Now try to stack three slices on different slots");
            });
            instructionCount  = 3;
        }
    }

    public int CalculateSlices(){
        int size =0;
        foreach(List<GameObject> obj in GlobalData.globalList){
                size += obj.Count;
        }
        return size;
    }

    void DestroySlices(){
        foreach(List<GameObject> obj in GlobalData.globalList){
            foreach(GameObject slice in obj){
                Destroy(slice);
            }
        }
    }

    int instructionCount = 0;
    void verticalFuseCondition(){

        fuseCase = true;
        FuseMessage(instructionCount);
        if(GlobalData.isFirstFusionOver){
            Debug.Log("Fusion Over");
            stage++;
            _pause = true;
            fuseCase = false;
            return;
        }

        Debug.Log("CurrentVertSlice Name: " + (CurrentVertSlice != null ? CurrentVertSlice.name : "null"));
        if(CurrentVertSlice != null){
            
            isPlaced = CurrentVertSlice.GetComponent<PizzaParabola>().GetIsPlaced();
            if(!isPlaced)
                return;
            
            isPlaced = false;
            Debug.Log("Slices Count: " + CalculateSlices());
            Debug.Log("GlobalData.isFirstFusionOver: " + GlobalData.isFirstFusionOver);

            if(CalculateSlices() > 5){
                instructionCount = 1;
                DestroySlices();
                GlobalData.ResetGlobalList();
                slicesCount = 0;
            }
        }
        CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
    }

    private IEnumerator Walkthrough()
    {

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

            if(stage == 2){
                if(WalkthroughDone == false){
                    WalkthroughDone = true;
                    
                    Objects[GameObjectNames.PIZZA_PEEL].SetActive(true);
                    showPeel();_pause = true;
                }
            }

            if(stage == 3){
                if(WalkthroughDone == false){
                    
                    showPizzaSlice();_pause = true;
                }

                // For next stage
                pressSpace = true;
            }

            if(stage == 4){
                verticalFuseCondition();
            }

            if(stage == 5){
                pressSpace = false;
                pizzaSpawner.SetActive(false);
                Objects[GameObjectNames.CHEAT_SHEET].SetActive(true);
                Objects[GameObjectNames.PIZZA_PEEL].SetActive(false);
                Objects[GameObjectNames.PLATE].SetActive(false);
                DestroySlices();
                Destroy(pizzaSpawnerComponent.GetSpawnedSlice());
                Objects[GameObjectNames.ANCHORS].SetActive(false);
                Objects[GameObjectNames.INSTRUCTION_UI].SetActive(false);

                Objects[GameObjectNames.INTRO_UI].SetActive(true);
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("NOTE: Ways to make Horizontal Fuses");
                });
                _pause = true;
            }

            if(stage == 6){
                Objects[GameObjectNames.SCORE_UI].SetActive(true);
                
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("AIM: Gain the total SCORE needed to complete LEVEL");
                });
                _pause = true;
            }

            if(stage == 7){
                Objects[GameObjectNames.GOLD_UI].SetActive(true);
                Objects[GameObjectNames.BUTTON].SetActive(true);
                Objects[GameObjectNames.COLOR_CHANGER].SetActive(true);

                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("sell GOLD to USE POWER UPs");
                });
                _pause = true;
            }

            if(stage == 8){
                // Objects[GameObjectNames.PIZZA_PEEL].SetActive(false);
                
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("AIM: Gain the total score needed to complete the level");
                });
                _pause = true;
            }

            if(stage == 9){
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.setIntroInstruction("Cool Right!\n Press Enter to Start Level1");
                });
                _pause = true;
            }

            if(stage == 10){
                SceneManager.LoadScene(3);
            }
        }
    }

    void Start(){
        // Initialization
        Objects.Add(GameObjectNames.PIZZA_PEEL, GameObject.Find(GameObjectNames.PIZZA_PEEL));
        Objects.Add(GameObjectNames.PLATE, GameObject.Find(GameObjectNames.PLATE));
        Objects.Add(GameObjectNames.SCORE_UI, GameObject.Find(GameObjectNames.SCORE_UI));
        Objects.Add(GameObjectNames.LEVEL_UI, GameObject.Find(GameObjectNames.LEVEL_UI));
        Objects.Add(GameObjectNames.GOLD_UI, GameObject.Find(GameObjectNames.GOLD_UI));
        Objects.Add(GameObjectNames.COLOR_CHANGER, GameObject.Find(GameObjectNames.COLOR_CHANGER));
        Objects.Add(GameObjectNames.INSTRUCTION_UI, GameObject.Find(GameObjectNames.INSTRUCTION_UI));
        Objects.Add(GameObjectNames.CHEAT_SHEET, GameObject.Find(GameObjectNames.CHEAT_SHEET));
        Objects.Add(GameObjectNames.FLOW_UI, GameObject.Find(GameObjectNames.FLOW_UI));
        Objects.Add(GameObjectNames.ANCHORS, GameObject.Find(GameObjectNames.ANCHORS));
        Objects.Add(GameObjectNames.BUTTON, GameObject.Find(GameObjectNames.BUTTON));
        Objects.Add(GameObjectNames.INTRO_UI, GameObject.Find(GameObjectNames.INTRO_UI));

        foreach(KeyValuePair<string, GameObject> key in Objects){
            Objects[key.Key].SetActive(false);
        }

        Objects[GameObjectNames.INTRO_UI].SetActive(true);
        ui_handler = GameObject.Find("UIHandler");
        ui_flow = GameObject.FindWithTag("flowui");
        
        pizzaSpawner = GameObject.FindWithTag("Spawner");
        pizzaSpawnerComponent = pizzaSpawner.GetComponent<NewSliceSpawn>();
        pizzaSpawner.SetActive(false);
    }

    void Update()
    {   
        Debug.Log("stage: "+ stage + " ----->" + fuseCase + " ----->" + WalkthroughDone);
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
