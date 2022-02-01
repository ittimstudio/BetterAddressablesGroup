namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor.IMGUI.Controls;

    public sealed class PathTreeViewItem : TreeViewItem
    {
        public PathTreeViewItem(int id, int depth, string displayName)
        {
            this.id = id;
            this.depth = depth;
            this.displayName = displayName;
        }
    }
}
