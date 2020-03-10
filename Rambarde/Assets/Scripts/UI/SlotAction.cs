using System.Collections.Generic;
using Skills;
using UnityEngine;

namespace UI
{
    public struct SlotAction
    {
        public enum ActionType
        {
            Increment,
            Decrement,
            Shuffle,
            Sync
        }
    
        public ActionType Action { get; set; }
        public List<Skill> Skills  { get; set; }
    } 
}
