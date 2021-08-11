﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    public string variablePath;

    public void OnSolve() {
        Session.Instance.Set(variablePath, true);
        StartCoroutine(Close());
    }

    public void OnClose() {
        Destroy(gameObject);
    }

    IEnumerator Close() {
        yield return new WaitForSeconds(2);
        OnClose();
    }
}
