using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client 
{
    [SerializeField]
    private Characters.CharacterData _character;
    public Characters.CharacterData Character { get { return _character; } set { _character = value; } }
    [SerializeField]
    private int[] _skillWheel;
    public int[] SkillWheel { get { return _skillWheel; } set { _skillWheel = value; } }
    [SerializeField]
    private string _clientName;
    public string ClientName { get { return _clientName; } set { _clientName = value; } }

    public Client() { }

    public Client(Characters.CharacterData data, int[] skillIndex, string clientName)
    {
        Character = data;
        SkillWheel = skillIndex;
        ClientName = clientName;
    }
}
