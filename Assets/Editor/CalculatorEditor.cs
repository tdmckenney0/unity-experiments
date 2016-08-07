using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Calculator))]
public class CalculatorEditor : Editor {

    public override void OnInspectorGUI()
    {
        Calculator calc = (Calculator) target;

        calc.firstOperand = EditorGUILayout.IntField("firstOperand", calc.firstOperand);
        calc.secondOperand = EditorGUILayout.IntField("secondOperand", calc.secondOperand);

        EditorGUILayout.LabelField("Result", calc.add().ToString());
    }
}
