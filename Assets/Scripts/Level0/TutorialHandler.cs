using System.Threading;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;

public class TutorialHandler : MonoBehaviour
{

    public bool WalkthroughDone = false;    
    public bool FirstSlice = true;

    private GameObject ui_handler,ui_flow;
    private int stage = 1;

    private int nStages = 7;
    private GameObject pizzaSpawner;
    private NewSliceSpawnLevel0 pizzaSpawnerComponent;
    private float spawnerSpawnTime;
    private List<GameObject> slicesTemp = new List<GameObject>();

    private GameObject prevObj;
    private bool _pause = false;
    private bool pressSpace = false;
    private bool isPlaced = false;
    private bool blockSpace = false;
    private bool fuseCase = false;
    private int VertCount = 0;

    //For Vertical Fuse
    private int slicesSize = 0;

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
            x.SetTutorialInstruction("Pan has 6 slots where pizza can be placed");
        });
    }
    
    void showPizzaSlice(){

        Debug.Log("Showing Pizza Slice..");
        pizzaSpawnerComponent.NewSliceSpawnSeconds = 1;
        pizzaSpawnerComponent.SpawnRed = 0;
        pizzaSpawner.SetActive(true);
        
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Pizza Slice rotates until you hit space to throw the pizza");
        });
    }

    void pressSpaceInst(){
        
        ui_flow.SetActive(false);
        pizzaSpawnerComponent.NewSliceSpawnSeconds = 1;
        pizzaSpawnerComponent.NeedsNewSlice = 0;
        Debug.Log("Press Space to throw the Pizza");
        // slicesTemp.Add(pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().GetSpawnedSlice());
        prevObj = pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().GetSpawnedSlice();
        slicesTemp.Add(prevObj);
        pressSpace = true;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
            x.SetTutorialInstruction("Press Space to throw the Pizza");
        });
        slicesSize = slicesTemp.Count;
    }

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
    void verticalFuseCondition(int i){
        
        if(ui_flow.activeSelf == false){
            ui_flow.SetActive(true);
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                x.SetFlowInstruction("(Press Space to throw the Pizza)");
            });
        }

        fuseCase = true;
        blockSpace = true;

        // Debug.Log("Count:::: " + (prevObj == null));
        GameObject currObj = pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().GetSpawnedSlice();        
        Debug.Log("sliceCount: " + GlobalData.isFirstFusionOver);

        // Debug.Log("VertCount: " + VertCount);
        if(VertCount < 5 ){

            if(GlobalData.isFirstFusionOver == false){
                isPlaced = prevObj.GetComponent<PizzaParabolaLevel0>().GetIsPlaced();

                // Debug.Log("SLice: " + prevObj.name + "   " + prevObj.tag);
                if(isPlaced){
                    if(VertCount == 0){
                        FuseMessage(0);
                    }
                    currObj = pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().GetSpawnedSlice();
                    // Debug.Log(" Placedddddddd");
                    isPlaced = currObj.GetComponent<PizzaParabolaLevel0>().GetIsPlaced();;
                    prevObj = currObj;
                    slicesTemp.Add(prevObj);
                    slicesSize = CalculateSlices();
                    VertCount++;
                }

            }else{
                fuseCase = false;
                _pause = true;
                stage++;
            }
            
        }else{
            for(int j=0;j<slicesTemp.Count-1;j++)
                Destroy(slicesTemp[j]);

            slicesTemp.Clear();
            slicesTemp.Add(pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().GetSpawnedSlice());
            
            GlobalData.ResetGlobalList();
            FuseMessage(1);
            VertCount = 0;
        }
    }

    IEnumerator WaitFor(int seconds){
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator Walkthrough()
    {

        while (!_pause){
            yield return new WaitForSeconds(0.3f);

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
                }

                if(stage == 5){
                    pressSpaceInst();_pause = true;
                }

                if(stage == 6){
                    verticalFuseCondition(0);
                }

                if(stage == 7){
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

                if(stage == 8){
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Aim: is to gain the total score needed to complete the level");
                    });
                }

                if(stage == 9){
                    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null,(x, y) => {
                        x.SetTutorialInstruction("Cool Right! You are Ready!\n Press Enter to Start Level1");
                    });
                }

                if(stage == 10){
                    SceneManager.LoadScene(1);
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
        pizzaSpawnerComponent = pizzaSpawner.GetComponent<NewSliceSpawnLevel0>();
        spawnerSpawnTime = pizzaSpawnerComponent.NewSliceSpawnSeconds;

        pizzaSpawner.SetActive(false);
        // pizzaSpawner.GetComponent<NewSliceSpawnLevel0>().NeedsNewSlice = 0;
    }

    void Update()
    {   
        Debug.Log("stage: "+ stage + " ----->" + fuseCase);
        StartCoroutine(Walkthrough());
        if (((!pressSpace && Input.GetKeyDown(KeyCode.Return)) 
        || (pressSpace &&  Input.GetKeyDown(KeyCode.Space))) 
        && !fuseCase){
            if(!fuseCase)stage++;
            _pause = false;
        }
    }
}
