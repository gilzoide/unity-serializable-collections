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
            (SerializedProperty keyProperty, SerializedProperty valueProperty) = GetKeyValueProperties(property);

            GUIContent keyLabel = ShouldHideLabel(keyProperty) ? GUIContent.none : null;
            GUIContent valueLabel = ShouldHideLabel(valueProperty) ? GUIContent.none : null;

            if (IsPropertyInArray(property.propertyPath))
            {
                DrawKeyValuePair(position, keyProperty, valueProperty, keyLabel, valueLabel, DEFAULT_SEPARATOR);
            }
            else
            {
                Rect foldoutRect = new Rect(position.position, EditorStyles.foldout.CalcSize(label) + new Vector2(0, FOLDOUT_MARGIN));
                if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        Rect drawRect = new Rect(position.x, foldoutRect.yMax, position.width, position.height - foldoutRect.height);
                        DrawKeyValuePair(drawRect, keyProperty, valueProperty, keyLabel, valueLabel, DEFAULT_SEPARATOR);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            (SerializedProperty keyProperty, SerializedProperty valueProperty) = GetKeyValueProperties(property);

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

            using (new LabelWidth((keyWidth / 2f) + INDENT_MARGIN))
            {
                // key
                EditorGUI.PropertyField(keyRect, keyProperty, keyLabel, true);
                // separator
                using (new AbsoluteIndentLevel(0))
                {
                    EditorGUI.LabelField(separatorRect, separatorLabel);
                }
                // value
                using (new AbsoluteIndentLevel(1))
                {
                    EditorGUI.PropertyField(valueRect, valueProperty, valueLabel, true);
                }
            }
        }

        public static bool IsPropertyInArray(string propertyPath)
        {
            return propertyPath.EndsWith("]");
        }

        static (SerializedProperty, SerializedProperty) GetKeyValueProperties(SerializedProperty property)
        {
            SerializedProperty keyProperty = property.Copy();
            if (!keyProperty.NextVisible(true))
            {
                throw new System.InvalidOperationException($"Expected type '{property.type}' to have 2 serialized fields!");
            }
            SerializedProperty valueProperty = keyProperty.Copy();
            if (!valueProperty.NextVisible(false))
            {
                throw new System.InvalidOperationException($"Expected type '{property.type}' to have 2 serialized fields!");
            }
            return (keyProperty, valueProperty);
        }

        static bool ShouldHideLabel(SerializedProperty property)
        {
            return !property.hasVisibleChildren
                || property.propertyType == SerializedPropertyType.Vector2
                || property.propertyType == SerializedPropertyType.Vector2Int
                || property.propertyType == SerializedPropertyType.Vector3
                || property.propertyType == SerializedPropertyType.Vector3Int
                || property.propertyType == SerializedPropertyType.Quaternion
                || property.propertyType == SerializedPropertyType.Rect
                || property.propertyType == SerializedPropertyType.RectInt
                || property.propertyType == SerializedPropertyType.Bounds
                || property.propertyType == SerializedPropertyType.BoundsInt
                || property.propertyType == SerializedPropertyType.Hash128
                ;
        }
    }
}
