using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int index {get; private set;}

    public Text qtyText;

    public Image itemImage;

    public Button slotButton;

    ISlotListener listener;

    public void Initialize(int index, ISlotListener listener) {
        this.index = index;
        this.listener = listener;

        slotButton.onClick.AddListener(OnClick);
    }

    void OnClick() {
        listener.OnSlotClick(index);
    }
}
