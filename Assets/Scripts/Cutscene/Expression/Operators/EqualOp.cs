using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualOp : BinaryOp
{
    public int Priority => 2;

    public object Do(object a, object b) {
        if (a is null && b is null)
            return true;
        
        if (!(a is null) && !(b is null)) {
            if (a.GetType() == b.GetType())
                return a.Equals(b);
            
            // int를 float으로 바꿔 테스트
            if (a is float || b is float)
                return System.Convert.ToSingle(a) == System.Convert.ToSingle(b); // TODO: 실수 오차 처리
            
            return false;
        }
        // 둘 중 하나만 null
        object nonNull = (a is null) ? b : a;

        // Default Value로 테스트
        if (nonNull is bool)
            return (bool) nonNull == false;
        if (nonNull is int)
            return (int) nonNull == 0;
        if (nonNull is float)
            return (float) nonNull == 0f;
        
        throw new BSSyntaxException(-1, $"지원하지 않는 타입[{nonNull.GetType()}]입니다.");
    }
}
