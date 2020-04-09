namespace Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

#if UNITY_EDITOR

    [CustomEditor(typeof(WeaponDataList))]
    public class WeaponDataListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var so = new SerializedObject(target);
            so.Update();

            SerializedProperty prop = so.GetIterator();
            prop.NextVisible(true);
            for (; prop.NextVisible(false);)
            {
                DrawProperies(prop, true);
            }

            so.ApplyModifiedProperties();
        }

        void DrawProperies(SerializedProperty masterProperty, bool drawChildren) {
            var isArray = SerializedPropertyType.Generic == masterProperty.propertyType
                && masterProperty.isArray
                ;

            var i = -1;
            foreach (SerializedProperty prop in masterProperty)
            {
                i++;

                if (SerializedPropertyType.Generic != prop.propertyType)
                {
                    EditorGUILayout.PropertyField(prop);
                }
                else
                {

                    EditorGUILayout.BeginVertical();
                    prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName, true);

                    if (isArray) {
                        switch (EditorGUILayout.Popup(-1, new string[] { "Insert", "Move up", "Move down", "Remove"})) {
                            case 0: {
                                    masterProperty.InsertArrayElementAtIndex(i);
                                    break;
                            }
                            case 3: {
                                    masterProperty.DeleteArrayElementAtIndex(i);
                                    break;
                                }
                        }
                    }

                    if (prop.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperies(prop, true);
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.EndVertical();
                }
            }
        }
    }

#endif

}