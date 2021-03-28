using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ZIndexer : MonoBehaviour
{
    // ZIndex를 가진 오브젝트는 [+Z_INDEX_RANGE, -Z_INDEX_RANGE]범위에서 Z 좌표가 변경됨
    // 플레이어의 Z 좌표는 0으로 고정
    const float Z_INDEX_RANGE = 5f;

    List<ZIndex> depthObjects;
    Player player;

    void Start()
    {
        depthObjects = new List<ZIndex>();
        foreach (ZIndex zIndex in FindObjectsOfType<ZIndex>()) {
            depthObjects.Add(zIndex);
        }

        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZIndices();
    }

    void UpdateZIndices() {
        List<ZIndex> frontOfPlayer = new List<ZIndex>();
        List<ZIndex> behindOfPlayer = new List<ZIndex>();

        foreach (ZIndex zIndex in depthObjects) {
            if (zIndex.alwaysBehindFloor == player.currentFloor) {
                behindOfPlayer.Add(zIndex);
            } else if (zIndex.alwaysFrontFloor == player.currentFloor) {
                frontOfPlayer.Add(zIndex);
            } else if (zIndex.IsBehindOf(player.transform.position)) {
                behindOfPlayer.Add(zIndex);
            } else {
                frontOfPlayer.Add(zIndex);
            }
        }

        // 플레이어의 z좌표를 0으로 고정
        Vector3 position = player.transform.position;
        position.z = 0f;
        player.transform.position = position;

        // Sort by z-index
        frontOfPlayer.Sort((ZIndex a, ZIndex b) => {
            return a.z.CompareTo(b.z);
        });
        behindOfPlayer.Sort((ZIndex a, ZIndex b) => {
            return a.z.CompareTo(b.z);
        });

        for (int i = 0; i < frontOfPlayer.Count; i++) {
            position = frontOfPlayer[i].transform.position;
            position.z = -Z_INDEX_RANGE * (1f - (float) i / frontOfPlayer.Count);
            frontOfPlayer[i].transform.position = position;
        }
        for (int i = 0; i < behindOfPlayer.Count; i++) {
            position = behindOfPlayer[i].transform.position;
            position.z = Z_INDEX_RANGE * ((float) (i + 1) / behindOfPlayer.Count);
            behindOfPlayer[i].transform.position = position;
        }
    }
}
*/