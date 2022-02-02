namespace BetterAddressablesGroup.Editor
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor.AddressableAssets;
    using UnityEditor.AddressableAssets.Settings;
    using UnityEditor.IMGUI.Controls;
    using UnityEditor;
    using static LogUtils;
    using Newtonsoft.Json;

    public static class AASUtils
    {
        public static AddressableAssetSettings AASSettings => AddressableAssetSettingsDefaultObject.Settings;

        public static bool IsAASInited => AASSettings != default;

        public static void CreateAASSettings()
        {
            AddressableAssetSettingsDefaultObject.Settings =
                AddressableAssetSettings.Create(@"Assets/AddressableAssetsData", @"AddressableAssetSettings", true, true);
        }

        [MenuItem(@"Kevin/GetTreeViewItems")]
        public static List<TreeViewItem> GetTreeViewItems()
        {
            if (!IsAASInited) { return new List<TreeViewItem>(); }

            var sw = Stopwatch.StartNew();
            var beginDepth = 0;
            var resultList = new List<TreeViewItem>();
            var dic = new Dictionary<(int id, int depth, string displayName), TreeViewItem>();

            foreach (var g in AASSettings.groups)
            {
                AddGroupItems(g, g.Name.ToNameArray());
            }

            Log($"Reload spend > {sw.ElapsedMilliseconds} ms");
            return resultList;

            void AddGroupItem(AddressableAssetGroup group, int depth, string displayName)
            {
                var key = (group.Guid.GetHashCode(), depth, displayName);

                if (dic.ContainsKey(key)) { return; }

                dic[key] = new GroupTreeViewItem(group, depth, displayName);
                resultList.Add(dic[key]);
            }

            void AddPathItem(int depth, string[] nameArr, int nIndex)
            {
                var keyElement = nameArr.GetPathKeyElement(nIndex);
                var key = (keyElement.id, depth, keyElement.displayName);

                if (dic.ContainsKey(key)) { return; }

                dic[key] = new PathTreeViewItem(depth, nameArr, nIndex);
                resultList.Add(dic[key]);
            }

            void AddEntryItem(AddressableAssetEntry entry, int depth)
            {
                var key = (entry.guid.GetHashCode(), depth, entry.address);

                if (dic.ContainsKey(key)) { return; }

                dic[key] = new EntryTreeViewItem(entry, depth);
                resultList.Add(dic[key]);
            }

            void AddGroupItems(AddressableAssetGroup group, string[] displayNames)
            {
                var entryDepth = 0;
                for (var i = 0; i < displayNames.Length; i++)
                {
                    var n = displayNames[i];
                    var isLast = i == displayNames.Length - 1;
                    var depth = beginDepth + i;

                    if (!isLast)
                    {
                        AddPathItem(depth, displayNames, i);
                    }
                    else
                    {
                        entryDepth = depth + 1;
                        AddGroupItem(group, depth, n);
                    }
                }

                //if (!group.GetIsExpanded()) { return; }

                var eArray = group.entries.ToArray();

                for (var i = 0; i < eArray.Length; i++)
                {
                    AddEntryItem(eArray[i], entryDepth);
                }
            }
            
        }

        public static string[] ToNameArray(this string completeName)
            => completeName.Split(new string[] { Config.Instance.GroupSeprarator }, StringSplitOptions.None);

        public static (int id, string displayName) GetPathKeyElement(this string[] strArray, int index)
        {
            var subArr = strArray.Take(index + 1);
            var id = string.Join(Config.Instance.GroupSeprarator, subArr).GetHashCode();
            var displayName = strArray[index];
            return (id, displayName);
        }

        public static bool GetIsExpanded(this AddressableAssetGroup group, bool defaultValue = false)
                => EditorPrefs.GetBool($"BetterAddressablesGroup.Group.{group.Guid}", defaultValue);

        public static void SetIsExpanded(this AddressableAssetGroup group, bool value)
                => EditorPrefs.SetBool($"BetterAddressablesGroup.Group.{group.Guid}", value);
    }
}
