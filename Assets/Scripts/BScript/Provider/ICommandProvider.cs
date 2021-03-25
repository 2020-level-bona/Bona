using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandProvider
{
    IScriptCommand Next();
}
