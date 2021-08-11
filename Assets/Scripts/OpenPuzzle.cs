using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour
{
    public GameObject puzzlePrefab;

    public string condition;

    Trigger trigger;

    void Awake() {
        trigger = GetComponent<Trigger>();
    }

    void Start() {
        trigger.AddListener(Open);
    }

    void Open() {
        if (condition == null || condition.Length == 0 || Expression.CastAsBool(Expression.Eval(condition)))
            Instantiate(puzzlePrefab);
    }
}
