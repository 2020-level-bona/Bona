using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: 정말 별로 좋은 구현은 아닌듯
public class FollowCharacters : Follower
{
    List<Character> targets;

    public FollowCharacters(CameraController cameraController, params Character[] characters) : base(cameraController) {
        targets = new List<Character>(characters);
    }

    protected override Vector2 GetCurrentTarget() {
        targets.RemoveAll(x => !(bool) x.gameObject);
        
        if (targets.Count == 0)
            return GetCameraPosition();
        
        Bounds bounds = targets[0].GetBounds();
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;
        for (int i = 1; i < targets.Count; i++) {
            bounds = targets[i].GetBounds();
            min = Vector2.Min(min, bounds.min);
            max = Vector2.Max(max, bounds.max);
        }

        return cameraController.ClampCameraPosition((min + max) / 2f);
    }

    public void AddTarget(Character character) {
        if (!targets.Contains(character))
            targets.Add(character);
    }

    public void RemoveTarget(Character character) {
        if (targets.Contains(character))
            targets.Remove(character);
    }
}
