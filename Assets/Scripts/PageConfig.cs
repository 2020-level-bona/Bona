using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PageConfig {
    [SerializeField]
    public bool selected;

    [SerializeField]
    public string condition;
    
    [SerializeField]
    public bool hide;

    [SerializeField]
    public string position;

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public BScriptExecutor triggerExecutor;

    [SerializeField]
    public BScriptExecutor autoExecutor;
}
