using UnityEditor;
using UnityEngine;

namespace ObjectType
{
    [CustomEditor(typeof(ObjectTypeController),true),CanEditMultipleObjects]
    public class ObjectTypeControllerEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var objectType = (ObjectTypeController) target;
            
            
            if (GUILayout.Button("Update Type"))
            {
                objectType.SetObjectType(ObjectTypeLibrary.Find().FindObjectType(objectType.defaultType.typeName));
                EditorUtility.SetDirty(target);
                
                var listeners=objectType.GetComponentsInChildren<IObjectTypeListener>();
                foreach (var listener in listeners)
                {
                    var listenerObject = (MonoBehaviour) listener;
                    EditorUtility.SetDirty(listenerObject);
                }
            }

            if (GUILayout.Button("Randomize Type"))
            {
                Type randomType=ObjectTypeLibrary.Find().GetRandomType();
                objectType.defaultType = ObjectTypeEnum.GetEnum(randomType.typeName);
                objectType.SetObjectType(ObjectTypeLibrary.Find().GetRandomType());
                EditorUtility.SetDirty(target);
                
                var listeners=objectType.GetComponentsInChildren<IObjectTypeListener>();
                foreach (var listener in listeners)
                {
                    var listenerObject = (MonoBehaviour) listener;
                    EditorUtility.SetDirty(listenerObject);
                }       
            }
        }
    }
}