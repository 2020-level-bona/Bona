using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
      static DialogueManager instance = null;
      public static DialogueManager Instance()
      
      {
          return instance;
      }
	  
      void Awake () 
      {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
