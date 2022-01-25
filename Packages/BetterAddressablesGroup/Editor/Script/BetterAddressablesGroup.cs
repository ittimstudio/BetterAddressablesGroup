namespace BetterAddressablesGroup.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using static LogUtils;
    using static FileUtils;

    public static class BetterAddressablesGroup
    {
        [MenuItem(@"Window/Asset Management/Addressables/BetterGroups")]
        public static void Open()
        {
            Config.Reload();
            Debug.Log(Config.Instance.Version);
        }
    }
}