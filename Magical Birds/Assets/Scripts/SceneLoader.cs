using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //added


public class SceneLoader : MonoBehaviour{

    public StateManager sm;

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
       //SceneManager.LoadScene(sceneBuildIndex:0);
        sm.setScene(1);
   }
   
   public void LoadSpring()
   {
        //SceneManager.LoadScene(sceneBuildIndex:1);
        sm.setScene(2);
   }
    public void LoadSummer()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
    public void LoadFall()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }
    public void LoadWinter()
    {
        SceneManager.LoadScene(sceneBuildIndex: 4);
    }
}
