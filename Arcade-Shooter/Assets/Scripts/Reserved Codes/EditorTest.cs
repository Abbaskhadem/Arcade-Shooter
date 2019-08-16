//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.PackageManager.UI;
//using UnityEngine.UIElements;

//public class EditorTest : EditorWindow
//{
//    private Vector3 MyPos;
//    private bool isEditing;
//    [MenuItem("Window/MySpecialEdit")]
//    static void Init()
//    {
//        EditorTest window = (EditorTest) EditorWindow.GetWindow(typeof(EditorTest));
//        window.Show();
//    }
//
//    void OnEnable()
//    {
//        if (!Application.isEditor)
//        {
//            Destroy((this));
//        }
//
//        SceneView.duringSceneGui += OnScene;
//    }
//
//    private void OnGUI()
//    {
//        GUILayout.BeginArea(new Rect(10,10,100,100));
//     //   GUILayout.BeginVertical("title", Box);
//        if (GUI.Button(new Rect(10, 10, 100, 100), "Create"+""+"Object"))
//        {
//            var g= new GameObject("testGameObject");
//            g.transform.position=Vector3.zero;
//        }
//     //   GUI.BeginGroup(new Rect(10,10,100,+200));
//        MyPos = EditorGUILayout.Vector3Field("v", MyPos);
//        Handles.EndGUI();
//    }
//
//    void OnScene(SceneView Scene)
//    {
//        
//        Event e = Event.current;
//        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
//        {
//            isEditing = true;
//        }
//        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Space)
//        {
//            isEditing = false;
//        }
//
//        if (isEditing)
//        {
//            EditorGUI.BeginChangeCheck();
//            MyPos = Handles.PositionHandle(MyPos, Quaternion.identity);
//            if (Selection.activeTransform != null)
//            {
//                Selection.activeTransform.position = MyPos;
//            }
//            if (EditorGUI.EndChangeCheck())
//            {
//                Debug.Log("End");
//            }
//            SceneView.RepaintAll();
//        }
//
//        if (e.type == EventType.MouseMove)
//        {
//            Debug.Log(("MOUSE MOVE") );
//        }
//    }
//
//}
