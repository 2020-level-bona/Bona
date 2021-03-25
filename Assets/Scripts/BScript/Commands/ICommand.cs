using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    bool Blocking {get;}
    int LineNumber {get;}
}
