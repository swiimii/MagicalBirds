using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CSVScript : MonoBehaviour {

    public string data_location;
    private List<string>[] data;
    private StreamReader sr;

    public void SelectFile()
    {
        try
        {
            data_location = Application.dataPath + "GameData";
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
    
    public List<string>[] ReadFile()
    {
        
        //becomes false if exception occurs
        bool success = true;

        //created list to be returned
        List<string>[] data = { new List<string>(), new List<string>(), new List<string>(), new List<string>() };
        
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
        if(success)
        {
            string line;
            char delims = ','; //denotes how the file's data is separated
            string[] tokens; //briefy holds the separated data

            //Splits each line of the file, and appends both halves into data lists
            while ((line = sr.ReadLine()) != null)
            {
                tokens = line.Split(delims); //putting separated data into "data" to be returned
                /*
                data[0].Add(tokens[0]);
                data[1].Add(tokens[1]);
                data[2].Add(tokens[2]);
                data[3].Add(tokens[3]);
                */
                int columnNumber = tokens.Length;
                for (int i = 0; i < 4; i++) // There are 4 potential values in the csv file : name, ID, rating, tag
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
            this.data = null;
            return null;
        }
    }

    public void WriteDefaultFile()
    {

        for(int i = 1; i <= 4; i++)
        {
            //string line = 
        }
    }

    


}

