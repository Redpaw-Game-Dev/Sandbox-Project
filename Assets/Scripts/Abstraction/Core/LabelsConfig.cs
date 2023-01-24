using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Scripts.Abstraction
{
    [CreateAssetMenu(menuName = "Game/Labels Config")]
    public partial class LabelsConfig : SerializedScriptableObject
    {
        [OdinSerialize, Searchable] private List<ILabel> _labels = new List<ILabel>();
    }
}