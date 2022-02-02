namespace BetterAddressablesGroup.Editor
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.IMGUI.Controls;
	using UnityEngine.Assertions;
    using static AASUtils;
    using static LogUtils;
    using Object = System.Object;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

    public sealed class AddressablesTreeView : TreeView
    {
		private enum Columns
		{
			Icon1,
			Icon2,
			Name,
			Value1,
			Value2,
			Value3,
		}

		public AddressablesTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
        {
        }

		public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
		{
			var columns = new[]
			{
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByLabel"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "),
					contextMenuText = "Asset",
					headerTextAlignment = TextAlignment.Center,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Right,
					width = 30,
					minWidth = 30,
					maxWidth = 60,
					autoResize = false,
					allowToggleVisibility = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "Sed hendrerit mi enim, eu iaculis leo tincidunt at."),
					contextMenuText = "Type",
					headerTextAlignment = TextAlignment.Center,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Right,
					width = 30,
					minWidth = 30,
					maxWidth = 60,
					autoResize = false,
					allowToggleVisibility = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Name"),
					headerTextAlignment = TextAlignment.Left,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Center,
					width = 150,
					minWidth = 60,
					autoResize = false,
					allowToggleVisibility = false
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Multiplier", "In sed porta ante. Nunc et nulla mi."),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 110,
					minWidth = 60,
					autoResize = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Material", "Maecenas congue non tortor eget vulputate."),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 95,
					minWidth = 60,
					autoResize = true,
					allowToggleVisibility = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Note", "Nam at tellus ultricies ligula vehicula ornare sit amet quis metus."),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 70,
					minWidth = 60,
					autoResize = true
				}
			};

			Assert.AreEqual(columns.Length, Enum.GetValues(typeof(Columns)).Length, "Number of columns should match number of enum values: You probably forgot to update one of them.");

			var state = new MultiColumnHeaderState(columns);
			return state;
		}

		protected override bool CanStartDrag(CanStartDragArgs args)
		{
			return !(args.draggedItem is PathTreeViewItem);
		}

		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
        {
			DragAndDrop.PrepareStartDrag();
			
		}

        

        protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
        {
            return base.HandleDragAndDrop(args);
        }


        

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(0, -1);
            var items = GetTreeViewItems();
            SetupParentsAndChildrenFromDepths(root, items);
            return root;
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            base.SelectionChanged(selectedIds);

            var items = selectedIds
                .Select(id => FindItem(id, rootItem))
                .Where(x => x is GroupTreeViewItem || x is EntryTreeViewItem)
                .ToArray();

			Log(string.Join(@", ", selectedIds));

			if (items.Length != 1) { return; }

            var item = FindItem(selectedIds[0], rootItem);

            if (item is GroupTreeViewItem gItem)
            {
                Selection.activeObject = gItem.Group;
            }
            else if (item is EntryTreeViewItem eItem)
            {
                Selection.activeObject = eItem.Entry.MainAsset;
            }
            else
            {
                return;
            }
        }
    }
}
