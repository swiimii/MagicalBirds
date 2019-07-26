using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CSVScript : MonoBehaviour {

    public string data_location;
    private List<string>[] data; // First col is label, second col is data
    private StreamReader sr;
    public bool fileIsFound = false;

    public void Awake()
    {
        SelectFile();
    }

    public void SelectFile()
    {
        print(data_location);
        try
        {
            data_location = Application.persistentDataPath + "\\GameData.csv";
            sr = new StreamReader(data_location);
            sr.Close();
            fileIsFound = true;
            print(data_location);
            // File is present
        }

        catch (System.Exception) //file is not present
        {
            print("Couldn't find save file. New file will be created.");
            WriteDefaultFile();
        }        
    }
    
    public List<string>[] ReadFile() // First col is label, second col is data
    {
        try
        {
            //becomes false if exception occurs
            bool success = true;

            //created list to be returned
            List<string>[] data = { new List<string>(), new List<string>()};

            //try to open file
            try
            {
                sr = new StreamReader(data_location);
            }
            catch (System.Exception)
            {
                success = false;
                return null;
            }

            //if no exception is found when opening file
            if (success)
            {
                string line;
                char delims = ','; //denotes how the file's data is separated
                string[] tokens; //briefy holds the separated data

                //Splits each line of the file, and appends both halves into data lists
                while ((line = sr.ReadLine()) != null)
                {

                    tokens = line.Split(delims); //putting separated data into "data" to be returned

                    int columnNumber = tokens.Length;
                    for (int i = 0; i < 2; i++) // There are 2 potential values in the csv file : name, ID, rating, tag
                    {
                        if (i < data.Length && i < columnNumber)
                        {
                            data[i].Add(tokens[i]);
                        }
                        else
                        {
                            data[i].Add("");
                        }
                    }
                }

                sr.Close();
                return data;
            }
            else
            {
                print("Failure");
                this.data = null;
                return null;
            }
        }
        catch (System.Exception e)
        {
            print("Error reading file. File copied, and new save file will be created");
            print(e);

            //TODO: Make copy file data into new file called "Save_Data_failure_backup". 
            File.AppendAllText(Application.persistentDataPath + "\\GameData_Failure_Backup.csv", File.ReadAllText(data_location));
            WriteDefaultFile();
            sr.Close();
            return ReadFile();
        }
    }

    public void WriteDefaultFile()
    {
        
        string output = string.Format("Unlocked Levels, 0 {0} Unlocked Abilities, 0 {0} Master Volume, 1 {0} Music Volume, 1 {0} Effects Volume, 1", System.Environment.NewLine);
        File.WriteAllText(data_location, output);                  
    }

    public void SaveGameState()
    {
        var gm = GetComponent<StateManager>();
        string output = string.Format("Unlocked Levels, {1} {0} Unlocked Abilities, {2} {0} Master Volume, {3} {0} Music Volume, {4} {0} Effects Volume, {5}", 
            System.Environment.NewLine, gm.unlockedLevels, gm.unlockedAbilities, gm.masterVolume, gm.musicVolume, gm.effectsVolume);
        File.WriteAllText(data_location, output);

    }

    public void DebugPrint()
    {
        for(int i = 0; i < data.Length; i++)
        {
            for(int j = 0; j < data[0].Count; j++)
            {
                print(data[i][j]);
            }
        }
    }



}

