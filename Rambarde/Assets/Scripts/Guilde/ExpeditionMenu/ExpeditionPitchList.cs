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
        string forestPath = "Assets/Resources/Guilde/ExpeditionMenu/pitchForest.txt";
        string cryptPath = "Assets/Resources/Guilde/ExpeditionMenu/pitchCrypt.txt";

        StreamReader reader = new StreamReader(forestPath);
        while (reader.Peek() >= 0)
            pitchForest.Add(reader.ReadLine());
        reader.Close();

        reader = new StreamReader(cryptPath);
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
