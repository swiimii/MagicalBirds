
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader fader;
    public GameObject levelsParent;



    public void Start()
    {
        var sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
        if (sm) print("Found");
        var levelIndex = sm.unlockedLevels;
        print(levelIndex);
        

        for(int i = 0; i < levelIndex; i++)
        {
            levelsParent.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        
    }

    

    // void Start()
    // {
    //     int levelReached = PlayerPrefs.GetInt("levelReached", 1);

    //     for (int i = 0; i < levelButtons.Length; i++)
    //     {
    //         if (i + 1 > levelReached)
    //             levelButtons[i].interactable = false;
    //     }
    // }

    public void ContinueGame()
    {
        var sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
        fader.FadeTo(sm.unlockedLevels + 2);
    }

    public void Select(int levelIndex)
    {
        fader.FadeTo(levelIndex);
    }



}