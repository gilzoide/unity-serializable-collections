using UnityEditor;
using UnityEngine;

namespace Gilzoide.SerializableCollections.Editor
{
    [CustomPropertyDrawer(typeof(ISingleFieldDrawable), true)]
    public class SingleFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty field = GetSingleField(property);
            EditorGUI.PropertyField(position, field, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty field = GetSingleField(property);
            return EditorGUI.GetPropertyHeight(field, label, true);
        }

        static SerializedProperty GetSingleField(SerializedProperty property)
        {
            SerializedProperty field = property.Copy();
            field.NextVisible(true);
            return field;
        }
    }
}
