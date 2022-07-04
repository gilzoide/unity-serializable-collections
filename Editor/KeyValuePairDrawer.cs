using UnityEditor;
using UnityEngine;

namespace Gilzoide.SerializableCollections.Editor
{
    [CustomPropertyDrawer(typeof(IKeyValuePairDrawable), true)]
    public class KeyValuePairDrawer : PropertyDrawer
    {
        const float FOLDOUT_MARGIN = 8;
        const float INDENT_MARGIN = 15;
        static readonly GUIContent DEFAULT_SEPARATOR = new GUIContent("=");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty keyProperty = property.FindPropertyRelative("Key");
            SerializedProperty valueProperty = property.FindPropertyRelative("Value");

            GUIContent keyLabel = keyProperty.hasVisibleChildren ? null : GUIContent.none;
            GUIContent valueLabel = valueProperty.hasVisibleChildren ? null : GUIContent.none;

            if (IsPropertyInArray(property.propertyPath))
            {
                DrawKeyValuePair(position, keyProperty, valueProperty, keyLabel, valueLabel, DEFAULT_SEPARATOR);
            }
            else
            {
                Rect foldoutRect = new Rect(position.position, EditorStyles.foldout.CalcSize(label) + new Vector2(0, FOLDOUT_MARGIN));
                if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
                {
                    EditorGUI.indentLevel++;

                    Rect drawRect = new Rect(position.x, foldoutRect.yMax, position.width, position.height - foldoutRect.height);
                    DrawKeyValuePair(drawRect, keyProperty, valueProperty, keyLabel, valueLabel, DEFAULT_SEPARATOR);

                    EditorGUI.indentLevel--;
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty keyProperty = property.FindPropertyRelative("Key");
            SerializedProperty valueProperty = property.FindPropertyRelative("Value");

            bool isInArray = IsPropertyInArray(property.propertyPath);

            float height = 0;
            if (!isInArray)
            {
                height += EditorGUI.GetPropertyHeight(property, false);
                if (property.isExpanded)
                {
                    height += FOLDOUT_MARGIN;
                }
            }
            if (isInArray || property.isExpanded)
            {
                height += Mathf.Max(EditorGUI.GetPropertyHeight(keyProperty, true), EditorGUI.GetPropertyHeight(valueProperty, true));
            }
            return height;
        }

        public static void DrawKeyValuePair(
            Rect position,
            SerializedProperty keyProperty,
            SerializedProperty valueProperty,
            GUIContent keyLabel,
            GUIContent valueLabel,
            GUIContent separatorLabel)
        {
            Vector2 separatorSize = EditorStyles.label.CalcSize(separatorLabel);
            float keyWidth = (position.width - separatorSize.x) / 2 - INDENT_MARGIN;

            Rect keyRect = new Rect(position.x, position.y, keyWidth, position.height);
            Rect separatorRect = new Rect(keyRect.xMax + INDENT_MARGIN, position.y, separatorSize.x, separatorSize.y);
            Rect valueRect = new Rect(separatorRect.xMax, position.y, keyWidth + INDENT_MARGIN, position.height);

            EditorGUI.PropertyField(keyRect, keyProperty, keyLabel, true);

            if (!valueProperty.hasVisibleChildren)
            {
                using (AbsoluteIndentLevel(0))
                {
                    EditorGUI.LabelField(separatorRect, separatorLabel);
                }
            }
            using (AbsoluteIndentLevel(1))
            {
                EditorGUI.PropertyField(valueRect, valueProperty, valueLabel, true);
            }
        }

        public static EditorGUI.IndentLevelScope AbsoluteIndentLevel(int indentLevel)
        {
            return new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel + indentLevel);
        }

        public static bool IsPropertyInArray(string propertyPath)
        {
            return propertyPath.EndsWith("]");
        }
    }
}
