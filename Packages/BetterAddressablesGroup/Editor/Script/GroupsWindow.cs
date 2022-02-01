namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using static AASUtils;

    internal sealed class GroupsWindow : EditorWindow
    {
        [SerializeField]
        private Vector2 contentScrollPos;

#if !UNITY_EDITOR_OSX
        private float ScreenW => Screen.width;
        private float ScreenH => Screen.height;
#else
        private float ScreenW => Screen.width * 0.5f;
        private float ScreenH => Screen.height * 0.5f;
#endif


        public void OnGUI()
        {
            DrawToolBar();
            DrawMainContent();
        }

        private void DrawToolBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.Width(ScreenW));
            {
                if (GUILayout.Button(@"Function", EditorStyles.toolbarButton, GUILayout.Width(60f)))
                {
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawMainContent()
        {
            if (!DrawOnAASNotInited())
            {
                return;
            }

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            contentScrollPos = EditorGUILayout.BeginScrollView(contentScrollPos);
            {
                
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            bool DrawOnAASNotInited()
            {
                if (!IsAASInited)
                {
                    if (GUILayout.Button(@"Create Addressables Settings"))
                    {
                        CreateAASSettings();
                    }

                    var oriLabelWordWarp = EditorStyles.whiteLargeLabel.wordWrap;
                    var gap = 30f;
                    var msg = "Click the \"Create\" button above or simply drag an asset into this window to start using Addressables.";
                    msg += "\n\nOnce you begin, the Addressables system will save some assets to your project to keep up with its data.";
                    var w = ScreenW - gap * 2.5f;
                    EditorStyles.whiteLargeLabel.wordWrap = true;

                    EditorGUILayout.Space(gap);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box(@"", GUILayout.Width(gap), GUILayout.Height(gap));
                    GUILayout.Label(msg, EditorStyles.whiteLargeLabel, GUILayout.Width(w), GUILayout.Height(ScreenH));
                    GUILayout.Box(@"", GUILayout.Width(gap), GUILayout.Height(gap));
                    EditorGUILayout.EndHorizontal();

                    EditorStyles.whiteLargeLabel.wordWrap = oriLabelWordWarp;
                }
                return IsAASInited;
            }
        }
    }
}