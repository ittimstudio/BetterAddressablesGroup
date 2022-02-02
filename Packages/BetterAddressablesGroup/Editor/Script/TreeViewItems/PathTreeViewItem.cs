namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor.AddressableAssets.Settings;
    using UnityEditor.IMGUI.Controls;

    public sealed class PathTreeViewItem : TreeViewItem
    {        
        public PathTreeViewItem(int depth, string[] pathNameArray, int pathNameIndex)
        {
            var keyElement = pathNameArray.GetPathKeyElement(pathNameIndex);
            id = keyElement.id;
            this.depth = depth;
            displayName = keyElement.displayName;
        }
    }
}
