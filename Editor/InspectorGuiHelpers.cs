using System;
using UnityEditor;

namespace Gilzoide.SerializableCollections.Editor
{
    public class AbsoluteIndentLevel : EditorGUI.IndentLevelScope
    {
        public AbsoluteIndentLevel(int indentLevel) : base(-EditorGUI.indentLevel + indentLevel) {}
    }

    public class LabelWidth : IDisposable
    {
        float _previousValue;

        public LabelWidth(float width)
        {
            _previousValue = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = width;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = _previousValue;
        }
    }
}
