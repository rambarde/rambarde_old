using System.Collections;
using System.Collections.Generic;
using Characters;
using Combat.Characters;
using UnityEngine;

public class Client : CharacterBase
{
    public Characters.Equipment[] equipment;

    public Client() { }

    public Client(CharacterData data, int[] skillIndex, string clientName)
    {
        Character = data;
        SkillWheel = skillIndex;
        _name = clientName;
    }
}
