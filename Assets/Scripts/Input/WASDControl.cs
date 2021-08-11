using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class WASDControl : MonoBehaviour
{
    Movable movable;
    public string condition;

    void Awake() {
        movable = GetComponent<Movable>();
    }

    public bool IsAvailable() {
        return condition == null || condition.Length == 0 || Expression.CastAsBool(Expression.Eval(condition));
    }

    void Update() {
        if (!ScriptSession.IsPlayingCutscene && IsAvailable()) {
            movable.MoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            EventManager.Instance.OnPlayerMove?.Invoke(transform.position);
        }
    }
}
