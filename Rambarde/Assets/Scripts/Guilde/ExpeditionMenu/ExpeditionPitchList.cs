using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionPitchList
{
    private List<string> pitchForest = new List<string>();
    private List<string> pitchCrypt = new List<string>();

    public void Init()
    {
        //string forestPath = Application.dataPath + "/Resources/Guilde/ExpeditionMenu/pitchForest";//.txt";
        //string cryptPath = Application.dataPath + "/Resources/Guilde/ExpeditionMenu/pitchCrypt";//.txt";        
        
        string forestPath = "Guilde/ExpeditionMenu/pitchForest";
        string cryptPath = "Guilde/ExpeditionMenu/pitchCrypt";

        StringReader reader = new StringReader(Resources.Load<TextAsset>(forestPath).text);
        //Resources.Load<TextAsset>(cryptPath);

        //StreamReader reader = new StreamReader(forestPath+".txt");
        while (reader.Peek() >= 0)
            pitchForest.Add(reader.ReadLine());
        reader.Close();

        //reader = new StreamReader(cryptPath + ".txt");
        reader = new StringReader(Resources.Load<TextAsset>(cryptPath).text);

        while (reader.Peek() >= 0)
            pitchCrypt.Add(reader.ReadLine());
        reader.Close();
    }

    public string expeditionPitch(ExpeditionMenu.QuestType type, int nPitch)
    {
        string pitch = "";
        
        switch (type)
        {
            case ExpeditionMenu.QuestType.Forest:
                pitch = pitchForest[nPitch];
                break;

            case ExpeditionMenu.QuestType.Crypt:
                pitch = pitchCrypt[nPitch];
                break;
        }

        return pitch;
    }
}
