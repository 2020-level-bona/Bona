using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public ItemType itemType;
    public int quantity = 1;

    Item item;

    Player player;
    Trigger trigger;

    void Awake() {
        player = FindObjectOfType<Player>();
        trigger = GetComponent<Trigger>();
    }

    void Start() {
        item = new Item(itemType, quantity);

        if (trigger)
            trigger.AddListener(Consume);
        else
            Debug.LogError("Trigger 컴포넌트가 없습니다. MouseClickTrigger 컴포넌트가 부착되어있는지 확인해주세요.");
    }

    void Consume() {
        player.inventory.AddItem(item);
        Destroy(gameObject);
    }
}
