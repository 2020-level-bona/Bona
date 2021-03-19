using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFairy : Character
{
    public override CharacterType type {
        get => CharacterType.WATERFAIRY;
    }

    public static readonly AnimatorState STAND = new AnimatorState("스탠딩");
    public static readonly AnimatorState WATER = new AnimatorState("물방울 만들며 장난치기");
}
