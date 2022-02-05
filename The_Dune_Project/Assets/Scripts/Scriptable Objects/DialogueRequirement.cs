using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Create Dialogue/dialogue settings")]
    public class DialogueRequirement:ScriptableObject
    {
        public string speakerName;
        public TextAsset inkDialogueFile;
        public List<AudioClip> audiosAssociatedWithDialogue;
    }
}
