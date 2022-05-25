using UnityEngine;

namespace Scriptable_Objects.Structures
{
    [CreateAssetMenu(fileName = "New Structure", menuName = "Structure")]
    public class StructureObject : ScriptableObject
    {
        public enum Type
        {
            Horde, Shop, Blacksmith, Enemy, Boss
        }

        public Type type;
        public GameObject prefab;
    }
}