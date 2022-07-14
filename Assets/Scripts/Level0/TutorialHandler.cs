using System.Globalization;
using System.ComponentModel;
using System.Timers;
using System;
using System.Threading;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;

public class TutorialHandler : MonoBehaviour
{

    public bool WalkthroughDone = false;    

    private GameObject ui_handler,ui_flow;
    private int stage = 1;

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

    void intro(){
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Lets take a brief Walk through!");
        });
    }

    void showPeel(){
        Debug.Log("Showing Peel..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Pizza Peel used to throw the pizza on to Pan");
        });
    }

    void showPan(){
        
        Debug.Log("Showing Pan..");

        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Pan has 6 slots where pizzas can be placed");
        });
        GlobalData.ValidSlices["Level0"] = new(){ SliceColor.Yellow };
    }
    
    delegate void Function();
    IEnumerator WaitFor(float seconds, Function toCall){
        yield return new WaitForSeconds(seconds);
        toCall();
    }

    void showPizzaSlice(){
        pizzaSpawnerComponent.NewSliceSpawnSeconds = 0;
        pizzaSpawner.SetActive(true);
        Debug.Log("Showing Pizza Slice..");

        // blockInput = true;
        // ui_flow.SetActive(false);
        
        // Time.timeScale = 0;
        // while(!pizzaSpawner.activeSelf){
        //     Debug.Log("Loading...");
            StartCoroutine(WaitFor(0.3f, () => {
                // ui_flow.SetActive(true);
                Debug.Log("Waiting for Pizza Spawner to be active");
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.SetTutorialInstruction("Pizza Slice rotates until you hit space to throw the pizza");
                });
                pressSpace = true;
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                    x.SetFlowInstruction("(Press Space to throw the Pizza)");
                });
            }));
        // }

        // For VerticalFuse verification in the next stage `verticalFuseCondition`
        CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
        if(CurrentVertSlice == null)
            Debug.Log("CurrentVertSlice is null");
        else
            Debug.Log("CurrentVertSlice is not null");

        Debug.Log("CurrentVertSlice: " + CurrentVertSlice.name);
        // blockIn = false;
    }

    // void pressSpaceInst(){
        
    //     ui_flow.SetActive(false);
    //     pizzaSpawnerComponent.NewSliceSpawnSeconds = 1;
    //     // pizzaSpawnerComponent.NeedsNewSlice = 0;
        
    //     // For VerticalFuse verification in the next stage `verticalFuseCondition`
    //     CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();

    //     pressSpace = true;
    //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
    //         x.SetTutorialInstruction("Press Space to throw the Pizza");
    //     });

    //     // Initialization for next stage
    //     GlobalData.isFirstFusionOver = false;
    // }

    void FuseMessage(int iMessageNumber){
        if(iMessageNumber == 0)
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Lets Try Vertical Fuse!\n Try to make stack of three slices with no more than 5 slices!");
            });

        if(iMessageNumber == 1)
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetTutorialInstruction("Try Again! \nTry throwing pizza on previous pizza");
            });
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

    void verticalFuseCondition(){
        pizzaSpawnerComponent.NewSliceSpawnSeconds = spawnerSpawnTime;
        
        // Time.timeScale = 1;
        // if(ui_flow.activeSelf == false){
        //     ui_flow.SetActive(true);
        //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
        //         x.SetFlowInstruction("(Press Space to throw the Pizza)");
        //     });
        // }
        FuseMessage(0);

        fuseCase = true;
        blockSpace = true;

        // isPlaced = 
        
        if(CurrentVertSlice != null){
            isPlaced = CurrentVertSlice.GetComponent<PizzaParabola>().GetIsPlaced();
            if(!isPlaced)
                return;
            
            isPlaced = false;
            Debug.Log("Slices Count: " + CalculateSlices());
            Debug.Log("GlobalData.isFirstFusionOver: " + GlobalData.isFirstFusionOver);

            if(CalculateSlices() <= 5){

                if(GlobalData.isFirstFusionOver == false){
                        Debug.Log("First Fusion Over" + GlobalData.isFirstFusionOver);
                        FuseMessage(0);
                }else{
                    Debug.Log("Fusion Over");
                    stage++;
                    _pause = true;
                    fuseCase = false;
                }
            }else{
                FuseMessage(1);
                DestroySlices();
                GlobalData.ResetGlobalList();
                slicesCount = 0;
            }
        }else{
            Debug.Log("CurrentVertSlice is null");
            CurrentVertSlice = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
            return;
        }
        

        // GameObject vertSliceObject = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();        
        
        // Debug.Log("GlobalData.isFirstFusionOver: " + GlobalData.isFirstFusionOver);
        // VertSlicesCount = CalculateSlices();
        // Debug.Log("VertSlicesCount: " + VertSlicesCount);

        // if(VertSlicesCount < 5 ){

        //     if(GlobalData.isFirstFusionOver == false){
        //         Debug.Log("FirstFusionNotOver");
        //         isPlaced = vertSliceObject.GetComponent<PizzaParabola>().GetIsPlaced();

        //         if(isPlaced){
        //             if(VertSlicesCount == 0){
        //                 FuseMessage(0);
        //             }
        //             Debug.Log("IsPlaced" + isPlaced);
        //             vertSliceObject = pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice();
        //             isPlaced = currObj.GetComponent<PizzaParabola>().GetIsPlaced();;
                    
        //             vertSlices.Add(vertSliceObject);
        //             VertSlicesCount += 1;
        //         }
        //     }else{
        //         Debug.Log("FirstFusionOver");
        //         fuseCase = false;
        //         _pause = true;
        //         stage++;
        //     }
        // }else{
        //     // for(int j=0;j<vertSlices.Count-1;j++)
        //     //     Destroy(vertSlices[j]);

        //     // vertSlices.Clear();
        //     // vertSlices.Add(pizzaSpawner.GetComponent<NewSliceSpawn>().GetSpawnedSlice());
            
        //     Debug.Log("Destroying all slices" + CalculateSlices());
        //     for(int j=0;j<GlobalData.globalList.Count;j++)
        //         for(int i=0;i<GlobalData.globalList[j].Count;i++)
        //             Destroy(GlobalData.globalList[j][i]);
        //     GlobalData.ResetGlobalList();

        //     FuseMessage(1);
        //     VertSlicesCount = 0;
        // }
    }

    private IEnumerator Walkthrough()
    {

        while (!_pause){
            yield return new WaitForSeconds(0.5f);

            if(!WalkthroughDone){
                
                if(stage == 1){
                    intro();_pause = true;
                }

                if(stage == 2){
                    showPeel();_pause = true;
                }

                if(stage == 3){
                    showPan();_pause = true;
                }

                if(stage == 4){
                    showPizzaSlice();_pause = true;

                    // For next stage
                    pressSpace = true;
                }

                // if(stage == 5){
                //     pressSpaceInst();_pause = true;
                // }

                if(stage == 5){
                    verticalFuseCondition();
                }

                if(stage == 6){
                    pressSpace = false;
                    Destroy(pizzaSpawnerComponent.GetSpawnedSlice());
                    pizzaSpawner.SetActive(false);
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetFlowInstruction("(Press Enter to Continue)");
                    });
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Tips: Make Horizontal Fuses to gain score");
                    });
                }

                if(stage == 7){
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Aim: is to gain the total score needed to complete the level");
                    });
                }

                if(stage == 8){
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Cool Right! You are Ready!\n Press Enter to Start Level1");
                    });
                }

                if(stage == 9){
                    SceneManager.LoadScene(3);
                }
                if(stage > 10){
                    WalkthroughDone = true;
                }
            }
        }
    }

    void Start(){
        ui_handler = GameObject.Find("UIHandler");
        ui_flow = GameObject.FindWithTag("flowui");
        pizzaSpawner = GameObject.FindWithTag("Spawner");
        pizzaSpawnerComponent = pizzaSpawner.GetComponent<NewSliceSpawn>();
        spawnerSpawnTime = pizzaSpawnerComponent.NewSliceSpawnSeconds;

        pizzaSpawner.SetActive(false);
        // pizzaSpawner.GetComponent<NewSliceSpawn>().NeedsNewSlice = 0;
    }

    void Update()
    {   
        Debug.Log("stage: "+ stage + " ----->" + fuseCase);
        StartCoroutine(Walkthrough());
        if (((!pressSpace && Input.GetKeyDown(KeyCode.Return)) 
        || (pressSpace &&  Input.GetKeyDown(KeyCode.Space))) 
        && !fuseCase && !blockInput){
            if(!fuseCase)stage++;
            _pause = false;
        }
    }
}
