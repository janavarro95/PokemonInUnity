using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Interactables
{
    [Serializable]
    public class DialogueEvent
    {
        public int index;
        public UnityEvent function;
        public bool hasTriggered;

        public DialogueEvent()
        {

        }

        public DialogueEvent(int index)
        {
            this.index = index;
            function = new UnityEvent();
            hasTriggered = false;
        }

        public void invoke()
        {
            hasTriggered = true;
            function.Invoke();
        }

        

    }
}
