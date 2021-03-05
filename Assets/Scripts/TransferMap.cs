using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Trigger))]
public class TransferMap : MonoBehaviour
{
    public SceneReference transferMap; // 이동할 맵

    Trigger trigger;

    void Awake() {
        trigger = GetComponent<Trigger>();
    }

    void Start()
    {
        trigger.AddListener(Transfer);
    }

    void Transfer() {
        SceneManager.LoadScene(transferMap);
    }
}
