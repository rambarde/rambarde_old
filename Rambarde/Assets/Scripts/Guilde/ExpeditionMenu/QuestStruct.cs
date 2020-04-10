using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestType
{
    FOREST, CRYPT
};

[System.Serializable] public struct Quest
{
    public string name;
    public QuestType type;
    public Sprite map;
    public Sprite upgradedMap;
    public string description;
    public bool isUpgradable;
}
