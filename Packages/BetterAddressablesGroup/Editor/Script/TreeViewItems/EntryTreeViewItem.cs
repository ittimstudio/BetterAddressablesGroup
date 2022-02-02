namespace BetterAddressablesGroup.Editor
{
    using UnityEditor.IMGUI.Controls;
    using UnityEditor.AddressableAssets.Settings;

    public sealed class EntryTreeViewItem : TreeViewItem
    {
        public AddressableAssetEntry Entry { get; private set; }

        public EntryTreeViewItem(AddressableAssetEntry entry, int depth)
        {
            Entry = entry;
            this.depth = depth;
            id = Entry.guid.GetHashCode();
            displayName = Entry.address;
        }
    }
}
