using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //added


public class SceneLoader : MonoBehaviour{
   
   public void LoadNextScene()
   {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex + 1);
      
   }
    
    public void OnTriggerEnter2D(Collider2D other) 
    {
        LoadNextScene();   
    }
    

   public void LoadStartScene()
   {
       SceneManager.LoadScene(sceneBuildIndex:0);
   }
}
