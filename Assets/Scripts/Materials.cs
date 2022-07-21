using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Materials : MonoBehaviour
{
    //there are four materials, yellow, red, yellow transparent, red transparent
    public Material[] MaterialList;
    Renderer Rend;
    MeshFilter Mf;
    NewSliceSpawn Nss;
    private int Red = 0;
    private int RedTopping = 1;
    private int Yellow = 2;
    private int YellowTopping = 3;
    private int Blue = 4;
    private int BlueTopping = 5;
    private int Green = 6;
    private int GreenTopping = 7;
    private int Brown = 8;
    private int BrownTopping = 9;
    private int Transparent = 10;
    private int Bomb = 11;
    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<Renderer>();
        Mf = GetComponent<MeshFilter>();
        Nss = GameObject.Find("PizzaSpawner").GetComponent<NewSliceSpawn>();
        Rend.enabled = true;
    }

    //TO COLOR
    public void ToYellow()
    {
        //Destroy(Mf.sharedMesh);
        Mf.sharedMesh = Nss.SliceColorToMesh(SliceColor.Yellow);
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[YellowTopping];
        l[1] = MaterialList[Yellow];
        Rend.sharedMaterials = l.ToArray();
    }
    public void ToRed()
    {
        //Destroy(Mf.sharedMesh);
        Mf.sharedMesh = Nss.SliceColorToMesh(SliceColor.Red);
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[RedTopping];
        l[1] = MaterialList[Red];
        Rend.sharedMaterials = l.ToArray();
    }
    public void ToBlue()
    {
        //Destroy(Mf.sharedMesh);
        Mf.sharedMesh = Nss.SliceColorToMesh(SliceColor.Blue);
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[BlueTopping];
        l[1] = MaterialList[Blue];
        Rend.sharedMaterials = l.ToArray();
    }
    public void ToDarkBrown()
    {
        //Destroy(Mf.sharedMesh);
        Mf.sharedMesh = Nss.SliceColorToMesh(SliceColor.DarkBrown);
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[BrownTopping];
        l[1] = MaterialList[Brown];
        Rend.sharedMaterials = l.ToArray();
    }
    public void ToGreen()
    {
        //Destroy(Mf.sharedMesh);
        Mf.sharedMesh = Nss.SliceColorToMesh(SliceColor.Green);
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[GreenTopping];
        l[1] = MaterialList[Green];
        Rend.sharedMaterials = l.ToArray();
    }

    //TO TRANSPARENT
    public void ToYellowTransparent()
    {
        ToTransparent();
    }
    public void ToRedTransparent()
    {
        ToTransparent();
    }
    public void ToBrownTransparent()
    {
        ToTransparent();
    }
    public void ToBlueTransparent()
    {
        ToTransparent();
    }
    public void ToDarkBrownTransparent()
    {
        ToTransparent();
    }
    public void ToGreenTransparent()
    {
        ToTransparent();
    }

    public void ToTransparent()
    {
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[Transparent];
        l[1] = MaterialList[Transparent];
        Rend.sharedMaterials = l.ToArray();
    }


    public void ToBomb()
    {
        List<Material> l = Rend.sharedMaterials.ToList();
        l[0] = MaterialList[Bomb];
        l[1] = MaterialList[Bomb];
        Rend.sharedMaterials = l.ToArray();
    }
    public void ToRainbow()
    {
        Rend.sharedMaterial = MaterialList[5];
    }
  
    public void FlipColor()
    {
        SliceColor c = GetComponent<PizzaRotation>().mColor;
        if(c == SliceColor.Red || c == SliceColor.Blue)
        {
            //Brown, Red slices will be turned yellow when colorchanger slice is thrown.
            ToYellow();
            GetComponent<PizzaRotation>().mColor = SliceColor.Yellow;
        }
        // else if(GetComponent<PizzaRotation>().IsBrown != 1)
        else
        {
            //Yellow slice
            ToRed();
            GetComponent<PizzaRotation>().mColor = SliceColor.Red;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
