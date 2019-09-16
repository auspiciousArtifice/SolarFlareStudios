using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MalbersAnimations.Controller
{

    [CustomPropertyDrawer(typeof(ModeProperties))]
    public class ModePropertiesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;


            EditorGUI.indentLevel = 0;



            #region Serialized Properties
            var Status = property.FindPropertyRelative("Status");
            var HoldByTime = property.FindPropertyRelative("HoldByTime");
            //var ModifyOnEnter = property.FindPropertyRelative("ModifyOnEnter");
            //var ModifyOnExit = property.FindPropertyRelative("ModifyOnExit");
            var affect = property.FindPropertyRelative("affect");
            var affectStates = property.FindPropertyRelative("affectStates");
            var OnEnter = property.FindPropertyRelative("OnEnter");
            var OnExit = property.FindPropertyRelative("OnExit");
            var ShowEvents = property.FindPropertyRelative("ShowEvents");

            #endregion

            var height = EditorGUIUtility.singleLineHeight;
            var line = new Rect(position);
            line.height = height;

            line.x += 4;
            line.width -= 8;

            EditorGUI.LabelField(line, label, EditorStyles.boldLabel);

            line.y += height + 2;

            //  var BoxPaint = line;

            //BoxPaint.x -= 4;
            //BoxPaint.width += 8;
            //BoxPaint.y -= 2;
            //BoxPaint.height += 4;


            //if (Status.intValue == 2) BoxPaint.height += height + 2;
            ////GUI.Box(BoxPaint, GUIContent.none, EditorStyles.helpBox);




            EditorGUI.PropertyField(line, Status);
            line.y += height + 2;

            if (Status.intValue == 2)
            {
                EditorGUI.PropertyField(line, HoldByTime);
                line.y += height + 2;
            }

            // line.y += 4;


            //float HeighMOEnter = EditorGUI.GetPropertyHeight(ModifyOnEnter);
            //float HeighMOExit = EditorGUI.GetPropertyHeight(ModifyOnExit);



            //var ModifiersLine = new Rect(line);
            //ModifiersLine.y += 2;



            //EditorGUI.PropertyField(ModifiersLine, ModifyOnEnter);
            //ModifiersLine.y += HeighMOEnter + 2;

            //EditorGUI.PropertyField(ModifiersLine, ModifyOnExit);
            //ModifiersLine.y += HeighMOExit + 4;

            //line.y = ModifiersLine.y;



            //BoxPaint = line;
            //BoxPaint.x -= 4;
            //BoxPaint.width += 8;
            //BoxPaint.y -= 2;
            //BoxPaint.height += 4;

            float ListHeight = EditorGUI.GetPropertyHeight(affectStates);
            //BoxPaint.height += ListHeight + 5;

            //  GUI.Box(BoxPaint, GUIContent.none, EditorStyles.helpBox);

            //EditorGUI.LabelField(line, affect.intValue == 0 ? "Include States" : "Exclude States", EditorStyles.boldLabel);
            //line.y += height + 2;

            line.y += 2;
            EditorGUI.PropertyField(line, affect);
            line.y += height + 2;

            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(line, affectStates, true);
            // line.y += 7;
            EditorGUI.indentLevel--;

            line.y += ListHeight;


            //BoxPaint = line;
            //BoxPaint.x -= 4; 
            //BoxPaint.width += 8;
            //BoxPaint.y -= 2;
            //BoxPaint.height -= 10;
            //BoxPaint.height += height + 2;


            //if (ShowEvents.boolValue)
            //{
            //    BoxPaint.height += EditorGUI.GetPropertyHeight(OnEnter);
            //    BoxPaint.height += EditorGUI.GetPropertyHeight(OnExit);
            //}


            //var NewEditorStyle =  new GUIStyle(  EditorStyles.foldout);
            //NewEditorStyle.fontStyle = FontStyle.Bold;
            //   GUI.Box(BoxPaint, GUIContent.none, EditorStyles.helpBox);


            EditorGUI.indentLevel++;
            ShowEvents.boolValue = EditorGUI.Foldout(line, ShowEvents.boolValue, new GUIContent("Events"), true);
            EditorGUI.indentLevel--;

            line.y += height + 2;

            if (ShowEvents.boolValue)
            {
                EditorGUI.PropertyField(line, OnEnter, true);
                line.y += EditorGUI.GetPropertyHeight(OnEnter);
                EditorGUI.PropertyField(line, OnExit, true);
            }




            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var Status = property.FindPropertyRelative("Status");
            //var ModifyOnEnter = property.FindPropertyRelative("ModifyOnEnter");
            //var ModifyOnExit = property.FindPropertyRelative("ModifyOnExit");
            var affectStates = property.FindPropertyRelative("affectStates");
            var ShowEvents = property.FindPropertyRelative("ShowEvents");
            var OnEnter = property.FindPropertyRelative("OnEnter");
            var OnExit = property.FindPropertyRelative("OnExit");

            var height = EditorGUIUtility.singleLineHeight;

            float TotalHeight = (height + 2) * 4 + 4; //Label and Status

            if (Status.intValue == 2)
            {
                TotalHeight += height + 2;
            }


            //TotalHeight += EditorGUI.GetPropertyHeight(ModifyOnEnter);
            //TotalHeight += EditorGUI.GetPropertyHeight(ModifyOnExit);
            TotalHeight += EditorGUI.GetPropertyHeight(affectStates);

            if (ShowEvents.boolValue)
            {
                TotalHeight += EditorGUI.GetPropertyHeight(OnEnter);
                TotalHeight += EditorGUI.GetPropertyHeight(OnExit);
            }

            return TotalHeight;
        }

    }
}