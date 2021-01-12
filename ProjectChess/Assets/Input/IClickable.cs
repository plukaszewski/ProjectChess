using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable : IInputSystemInerface
{
    void OnClick(int Button);
}
