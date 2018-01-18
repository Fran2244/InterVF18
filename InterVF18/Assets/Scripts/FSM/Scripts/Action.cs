using UnityEngine;

namespace AI
{

    public abstract class Action : ScriptableObject
    {
        public virtual void ActEnter(StateController controller) { }
        public abstract void Act(StateController controller);
        public virtual void ActExit(StateController controller) { }
    }

}
