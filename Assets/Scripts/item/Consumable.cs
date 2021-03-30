using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public ItemType itemType;
    public int quantity = 1;
    public string uniqueName = "No Name";

    public string saveIdentifier => $"i_{uniqueName}";

    Item item;

    Inventory inventory;
    Trigger trigger;

    void Awake() {
        trigger = GetComponent<Trigger>();
    }

    void Start() {
        inventory = FindObjectOfType<Game>().inventory;

        CheckUniqueName();

        if (HasConsumed()) {
            Destroy(gameObject);
            return;
        }

        item = new Item(itemType, quantity);

        if (trigger)
            trigger.AddListener(Consume);
        else
            Debug.LogError("Trigger 컴포넌트가 없습니다. MouseClickTrigger 컴포넌트가 부착되어있는지 확인해주세요.");
    }

    void Consume() {
        inventory.AddItem(item);
        SetAsConsumed();
        Destroy(gameObject);
    }

    void CheckUniqueName() {
        if (uniqueName == "No Name")
            throw new System.Exception("이름이 기본 이름인 'No Name'으로 설정되어 있습니다. 이름을 변경해주세요.");
        
        foreach (Consumable consumable in FindObjectsOfType<Consumable>()) {
            if (consumable == this)
                continue;
            if (consumable.uniqueName == uniqueName)
                throw new System.Exception($"동일한 이름({uniqueName})을 가진 두 Consumable이 존재합니다.");
        }
    }

    public bool HasConsumed() {
        return Session.CurrentScene.GetBool(saveIdentifier);
    }

    void SetAsConsumed() {
        Session.CurrentScene.Set(saveIdentifier, true);
    }
}
