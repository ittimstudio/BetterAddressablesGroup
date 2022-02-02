namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.IMGUI.Controls;
    using static AASUtils;

    internal sealed class GroupsWindow : EditorWindow
    {
        [SerializeField]
        private Vector2 contentScrollPos;

        [SerializeField]
        private TreeViewState treeViewState;

        [SerializeField]
        private MultiColumnHeaderState headerState;

        private TreeView treeView;
        private MultiColumnHeader header;

#if !UNITY_EDITOR_OSX
        private float ScreenW => Screen.width;
        private float ScreenH => Screen.height;
#else
        private float ScreenW => Screen.width * 0.5f;
        private float ScreenH => Screen.height * 0.5f;
#endif

        private void OnEnable()
        {
            if (headerState == null)
            {
                treeViewState = new TreeViewState();
                headerState = AddressablesTreeView.CreateDefaultMultiColumnHeaderState(ScreenW - 50f);
            }

            header = new MultiColumnHeader(headerState);
            treeView = new AddressablesTreeView(treeViewState, null);
            treeView.Reload();
        }

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

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(@"Reload", EditorStyles.toolbarButton, GUILayout.Width(50f)))
                {
                    treeView.Reload();
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

            EditorGUILayout.BeginVertical();
            contentScrollPos = EditorGUILayout.BeginScrollView(contentScrollPos);
            {
                var rect = new Rect(0f, 0f, position.width, position.height);
                treeView.OnGUI(rect);
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
                    EditorGUILayout.Space(gap);
                    GUILayout.Label(msg, EditorStyles.whiteLargeLabel, GUILayout.Width(w), GUILayout.Height(ScreenH));
                    EditorGUILayout.Space(gap);
                    EditorGUILayout.EndHorizontal();

                    EditorStyles.whiteLargeLabel.wordWrap = oriLabelWordWarp;
                }
                return IsAASInited;
            }
        }
    }
}