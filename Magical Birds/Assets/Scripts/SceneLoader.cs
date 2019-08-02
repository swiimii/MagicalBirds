using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //added


public class SceneLoader : MonoBehaviour{

    public StateManager sm;

    public void Start()
    {
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();

    }
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
        // SceneManager.LoadScene(sceneBuildIndex:2);
        sm.setScene(2);
   }
    public void LoadSummer()
    {
       // SceneManager.LoadScene(sceneBuildIndex: 2);
        sm.setScene(3);
    }
    public void LoadFall()
    {
       //SceneManager.LoadScene(sceneBuildIndex: 3);
        sm.setScene(4);
    }
    public void LoadWinter()
    {
        //SceneManager.LoadScene(sceneBuildIndex: 4);
        sm.setScene(5);
    }

    public void LoadSelectScene()
    {
        //SceneManager.LoadScene(sceneBuildIndex:0);
        sm.setScene(6);
    }
}
