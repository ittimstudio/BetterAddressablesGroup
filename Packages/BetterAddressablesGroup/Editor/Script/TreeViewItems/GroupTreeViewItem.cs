namespace BetterAddressablesGroup.Editor
{
    using UnityEditor.IMGUI.Controls;
    using UnityEditor.AddressableAssets.Settings;

    public sealed class GroupTreeViewItem : TreeViewItem
    {
        public AddressableAssetGroup Group { get; private set; }

        public GroupTreeViewItem(AddressableAssetGroup group, int depth, string displayName)
        {
            if (group == null)
            {
                displayName = @"Missing Group";
                id = displayName.GetHashCode();
                this.depth = depth;
                return;
            }

            Group = group;
            id = Group.Guid.GetHashCode();
            this.depth = depth;
            this.displayName = displayName;
        }
    }
}
