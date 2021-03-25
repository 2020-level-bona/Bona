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
            if (a is double || b is double)
                return System.Convert.ToDouble(a) == System.Convert.ToDouble(b); // TODO: 실수 오차 처리
            
            return false;
        }
        // 둘 중 하나만 null
        object nonNull = (a is null) ? b : a;

        // Default Value로 테스트
        if (nonNull is bool)
            return (bool) nonNull == false;
        if (nonNull is long)
            return (long) nonNull == 0;
        if (nonNull is double)
            return (double) nonNull == 0d;
        
        throw new BSSyntaxException(-1, $"지원하지 않는 타입[{nonNull.GetType()}]입니다.");
    }
}
