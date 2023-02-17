#if  UNITY_EDITOR
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;

namespace Sandbox.Abstraction
{
    public partial class LabelsConfig
    {
        private static LabelsConfig _instance;

        public static IEnumerable GetLabels()
        {
            if (_instance == null) SetInstance();
            ValueDropdownList<ILabel> labelsList = new ValueDropdownList<ILabel>();
            _instance?._labels.ForEach(label => labelsList.Add(new ValueDropdownItem<ILabel>(label.GetType().Name, label)));
            return labelsList;
        }
        
        public static ILabel GetLabel(ILabel labelToGet)
        {
            if (_instance == null) SetInstance();
            return _instance?._labels.FirstOrDefault(label => label.Equals(labelToGet));
        }
        
        private static void SetInstance()
        {
            var assets = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(LabelsConfig).Name);

            if (assets.Length > 0)
            {
                _instance = assets.Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                    .Select(path => UnityEditor.AssetDatabase.LoadAssetAtPath<LabelsConfig>(path)).First();
            }
        }
    }
}
#endif