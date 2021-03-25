using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryRenderer
{
    // 인벤토리를 다시 그려야 함을 알려준다.
    void Invalidate();
}
