using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
   public GameObject popUpBox;
   public Animator animator;
   public TMP_Text popUpText;

   public void PopUpScoreDialog(string points){
       popUpBox.SetActive(true);
       //popUpText=text;
       animator.SetTrigger("pop");
   }
}
