﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Move {

    public Run(string _name) : base(_name) { }

    override public Dictionary<string, int> processMove(Fighter attacker, Fighter target) {
        return new Dictionary<string, int>();
    }

    public override bool moveEligible(Fighter attacker) {
        return true;
    }
}
