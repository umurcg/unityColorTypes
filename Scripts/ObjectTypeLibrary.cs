using System;
using UnityEngine;

namespace ObjectType
{
    [CreateAssetMenu(fileName = nameof(ObjectTypeLibrary), menuName = "ObjectType/" + nameof(ObjectTypeLibrary))]
    public class ObjectTypeLibrary : ScriptableObject
    {
        public bool isDefault;
        public ObjectTypeController[] prefabs;
        public Type[] objectTypes;

        public static ObjectTypeLibrary Find(string name = nameof(ObjectTypeLibrary))
        {
            var allLibraries = Resources.LoadAll<ObjectTypeLibrary>("");
            foreach (var library in allLibraries)
            {
                if(library.isDefault) return library;
            }

            if (allLibraries.Length > 0)
            {
                Debug.LogWarning("No default library found, returning the first one found");
                return allLibraries[0];
            }
            
#if UNITY_EDITOR
            if (allLibraries.Length == 0)
            {
                //Create the library
                var library = CreateInstance<ObjectTypeLibrary>();
                library.objectTypes = Array.Empty<Type>();
                library.isDefault = true;

                //Save the library
                var path = "Assets/Resources/" + name + ".asset";
                UnityEditor.AssetDatabase.CreateAsset(library, path);

                //Refresh the database
                UnityEditor.AssetDatabase.Refresh();
                return library;
            }
#endif

            return null;
        }

        public Type FindObjectType(string typeName)
        {
            foreach (var objectType in objectTypes)
            {
                if (objectType.typeName == typeName)
                {
                    return objectType;
                }
            }

            return new Type();
        }

        public string[] GetObjectTypeNames()
        {
            var names = new string[objectTypes.Length];
            for (var i = 0; i < objectTypes.Length; i++)
            {
                names[i] = objectTypes[i].typeName;
            }

            return names;
        }

        public Type GetRandomType()
        {
            return objectTypes[UnityEngine.Random.Range(0, objectTypes.Length)];
        }
    }
}