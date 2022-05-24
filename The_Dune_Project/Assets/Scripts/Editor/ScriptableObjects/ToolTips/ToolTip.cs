using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Create ToolTip/text props")]
    public class ToolTip:ScriptableObject
    {
        public int id;
        public string message;
        public float durationTime ;
    }
}
