using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState
{
    public string Name { get; private set; }

    public int Hash { get; private set; }

    public bool ShouldFlip { get; private set; }

    public bool FlipValue { get; private set; }

    /// <summary>
    /// AnimatorController에서 재생될 수 있는 AnimatorState를 생성합니다.
    /// <paramref name="name"/>은 AnimatorController가 참조하는 Animator에 존재하는 애니메이션이어야 합니다.
    /// <paramref name="flip"/>에 값을 전달하면, 이 애니메이션이 재생될 때 스프라이트의 flipX값이 이 값으로 변경됩니다.
    /// 전달되지 않을 경우, 애니메이션이 재생되어도 이전의 flipX값을 그대로 유지합니다.
    /// </summary>
    /// <param name="name">재생할 애니메이션 파일 이름입니다.</param>
    public AnimatorState(string name) {
        Name = name;
        ShouldFlip = false;

        Hash = Animator.StringToHash(name);
    }

    /// <summary>
    /// AnimatorController에서 재생될 수 있는 AnimatorState를 생성합니다.
    /// <paramref name="name"/>은 AnimatorController가 참조하는 Animator에 존재하는 애니메이션이어야 합니다.
    /// <paramref name="flip"/>에 값을 전달하면, 이 애니메이션이 재생될 때 스프라이트의 flipX값이 이 값으로 변경됩니다.
    /// 전달되지 않을 경우, 애니메이션이 재생되어도 이전의 flipX값을 그대로 유지합니다.
    /// </summary>
    /// <param name="name">재생할 애니메이션 파일 이름입니다.</param>
    /// <param name="flip">스프라이트의 flipX에 사용될 값입니다.</param>
    public AnimatorState(string name, bool flip) : this(name) {
        ShouldFlip = true;
        FlipValue = flip;
    }
}
