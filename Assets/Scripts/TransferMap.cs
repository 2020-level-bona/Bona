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

    Trigger trigger;

    void Awake() {
        trigger = GetComponent<Trigger>();
    }

    void Start()
    {
        trigger.AddListener(Transfer);
    }

    void Transfer() {
        FindObjectOfType<Game>()?.TransferScene(targetScene);
    }
}
