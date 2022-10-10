using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageBase : MonoBehaviour
{
    public abstract void StageEnter();
    public abstract void StageUpdate();
    public abstract void StageExit();
}
