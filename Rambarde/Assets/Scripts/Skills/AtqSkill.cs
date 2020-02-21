﻿using System.Threading.Tasks;
using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Atq", menuName = "Skills/Atq")]
    public class AtqSkill : Skill {
        public override async Task Execute(Stats source, Character target) {
            if (Random.Range(0, 100) <= source.prec) {
                var dmg = Random.Range(0, 100) <= source.crit ? source.atq * 2 : source.atq;
                // Debug.Log("Damage inflicted: " + dmg);
                await target.TakeDamage(dmg);
            }
            else Debug.Log("Miss!");
        }
    }
}