using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Character
{
    public override CharacterType type {
        get => CharacterType.MOLE;
    }

    public static readonly AnimatorState EVIL = new AnimatorState("사악한표정");
    public static readonly AnimatorState DIE = new AnimatorState("찔린표정");
}
