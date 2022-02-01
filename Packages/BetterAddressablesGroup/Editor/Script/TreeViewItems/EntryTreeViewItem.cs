namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor.IMGUI.Controls;
    using UnityEditor.AddressableAssets.Settings;
    using static AASUtils;

    public sealed class EntryTreeViewItem : TreeViewItem
    {
        public static int GenerateID(AddressableAssetEntry entry)
            => entry.guid.GetHashCode();

        public AddressableAssetEntry Entry { get; private set; }

        public EntryTreeViewItem(AddressableAssetEntry entry, int depth)
        {
            Entry = entry;
            this.depth = depth;
            id = GenerateID(entry);
            displayName = Entry.address;
        }
    }
}
