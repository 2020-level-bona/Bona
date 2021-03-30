using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Trigger))]
public class TransferMap : MonoBehaviour
{
    [FormerlySerializedAs("transferMap")]
    public SceneReference targetScene; // 이동할 맵

    public string condition;

    Trigger trigger;

    void Awake() {
        trigger = GetComponent<Trigger>();
    }

    void Start()
    {
        trigger.AddListener(Transfer);
    }

    void Transfer() {
        if (condition == null || condition.Length == 0 || Expression.CastAsBool(Expression.Eval(condition)))
            FindObjectOfType<Game>()?.TransferScene(targetScene);
    }
}
