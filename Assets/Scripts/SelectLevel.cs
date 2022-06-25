using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    private void LoadLevel(int l)
    {
        Debug.Log("Clicked level button " + l.ToString());
    }

    public void L0()
    {
        LoadLevel(0);
    }
    public void L1()
    {
        LoadLevel(1);
    }
    public void L2()
    {
        LoadLevel(2);
    }
    public void L3()
    {
        LoadLevel(3);
    }
    public void L4()
    {
        LoadLevel(4);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
