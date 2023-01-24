using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Zenject;

namespace Scripts.Abstraction
{
    public abstract class Entity : SerializedMonoBehaviour, IEntity
    {
        [OdinSerialize, DictionaryDrawerSettings(IsReadOnly = true, DisplayMode = DictionaryDisplayOptions.OneLine)]
        protected Dictionary<ILabel, List<IFeature>> _labeledFeatures = new Dictionary<ILabel, List<IFeature>>();

        public Dictionary<ILabel, List<IFeature>> LabeledFeatures => _labeledFeatures;
        
        public event Action<ILabel, IFeature> OnFeatureAdded;
        public event Action<ILabel, IFeature> OnFeatureRemoved;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            foreach (var labeledFeaturesPair in _labeledFeatures)
            {
                labeledFeaturesPair.Value.ForEach(feature => diContainer.Inject(feature));
            }
        }

        public bool AddFeature(ILabel label, IFeature feature)
        {
            if (!_labeledFeatures.ContainsKey(label))
            {
                _labeledFeatures.Add(label, new List<IFeature>());
            }
            
            if (_labeledFeatures[label].Contains(feature))
            {
                return false;
            }
            else
            {
                _labeledFeatures[label].Add(feature);
                OnFeatureAdded?.Invoke(label, feature);
                return true;
            }
        }

        public void AddFeatures(Dictionary<ILabel, List<IFeature>> labeledFeaturesToAdd)
        {
            foreach (var labeledFeatures in labeledFeaturesToAdd)
            {
                foreach (var feature in labeledFeatures.Value)
                {
                    AddFeature(labeledFeatures.Key, feature);
                }
            }
        }

        public bool RemoveFeature<T>(ILabel label, Type featureType = null) where T : IFeature
        {
            if (!_labeledFeatures.ContainsKey(label)) return false;
            else
            {
                T featureToRemove = featureType != null
                    ? (T)_labeledFeatures[label].FirstOrDefault(feature => feature.GetType() == featureType)
                    : (T)_labeledFeatures[label].FirstOrDefault(feature => feature.GetType() == typeof(T));

                return RemoveFeature<T>(label, featureToRemove);
            }
        }

        public bool RemoveFeature<T>(ILabel label, T featureToRemove) where T : IFeature
        {
            if (!_labeledFeatures.ContainsKey(label)) return false;
            else
            {
                if (featureToRemove != null)
                {
                    _labeledFeatures[label].Remove(featureToRemove);
                    OnFeatureRemoved?.Invoke(label, featureToRemove);
                    return true;
                }
                return false;
            }
        }

        public void RemoveFeatures<T>(ILabel label, Type featuresType = null) where T : IFeature
        {
            if (_labeledFeatures.ContainsKey(label))
            {
                IEnumerable<T> featuresToRemove = featuresType != null
                    ? (IEnumerable<T>)_labeledFeatures[label].Where(feature => feature.GetType() == featuresType)
                    : (IEnumerable<T>)_labeledFeatures[label].Where(feature => feature.GetType() == typeof(T));

                if (featuresToRemove != null)
                {
                    featuresToRemove.ForEach(featureToRemove => RemoveFeature<T>(label, featureToRemove));
                }
            }
        }

        public IFeature GetFeature<T>(ILabel label, Type featureType = null) where T : IFeature
        {
            return _labeledFeatures.ContainsKey(label)
                ? featureType != null
                    ? (T)_labeledFeatures[label].FirstOrDefault(feature => feature.GetType() == featureType)
                    : (T)_labeledFeatures[label].FirstOrDefault(feature => feature.GetType() == typeof(T))
                : null;
        }

        public IEnumerable<T> GetFeatures<T>(ILabel label, Type featuresType = null) where T : IFeature
        {
            return _labeledFeatures.ContainsKey(label)
                ? featuresType != null
                    ? (IEnumerable<T>)_labeledFeatures[label].FindAll(feature => feature.GetType() == featuresType)
                    : (IEnumerable<T>)_labeledFeatures[label].FindAll(feature => feature.GetType() == typeof(T))
                : null;
        }

        public bool ContainsFeature<T>(ILabel label, Type featureType = null) where T : IFeature
        {
            return _labeledFeatures.ContainsKey(label) && (featureType != null
                ? _labeledFeatures[label].Any(feature => feature.GetType() == featureType)
                : _labeledFeatures[label].Any(feature => feature.GetType() == typeof(T)));
        }

#if UNITY_EDITOR
        [OdinSerialize, ValueDropdown("@LabelsConfig.GetLabels()"),
         InlineButton("RemoveLabelThroughEditor", "Remove"),
         InlineButton("AddLabelThroughEditor", "Add"),
         PropertyOrder(-1)]
        
        private ILabel _editorLabel;
        private void AddLabelThroughEditor()
        {
            if (!_labeledFeatures.ContainsKey(_editorLabel))
            {
                _labeledFeatures.Add(LabelsConfig.GetLabel(_editorLabel), new List<IFeature>());
            }
        }
        
        private void RemoveLabelThroughEditor()
        {
            if (_labeledFeatures.ContainsKey(_editorLabel))
            {
                _labeledFeatures.Remove(LabelsConfig.GetLabel(_editorLabel));
            }
        }
#endif
    }
}