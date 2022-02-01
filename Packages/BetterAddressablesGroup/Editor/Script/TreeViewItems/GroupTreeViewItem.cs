

namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor.IMGUI.Controls;
    using UnityEditor.AddressableAssets.Settings;

    public sealed class GroupTreeViewItem : TreeViewItem
    {
        public static int GenerateID(AddressableAssetGroup group)
            => group.Guid.GetHashCode();

        public AddressableAssetGroup Group { get; private set; }

        public GroupTreeViewItem(AddressableAssetGroup group, int depth, string displayName)
        {
            Group = group;
            id = GenerateID(Group);
            this.depth = depth;
            this.displayName = displayName;
        }
    }
}
