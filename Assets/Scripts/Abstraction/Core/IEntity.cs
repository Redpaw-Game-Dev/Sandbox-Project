using System;
using System.Collections.Generic;

namespace Sandbox.Abstraction
{
    public interface IEntity
    {
        public Dictionary<ILabel, List<IFeature>> LabeledFeatures { get; }

        public event Action<ILabel, IFeature> OnFeatureAdded;
        public event Action<ILabel, IFeature> OnFeatureRemoved;

        public bool AddFeature(ILabel label, IFeature feature);
        public void AddFeatures(Dictionary<ILabel, List<IFeature>> labeledFeaturesToAdd);
        public bool RemoveFeature<T>(ILabel label, Type featureType = null) where T : IFeature;
        public bool RemoveFeature<T>(ILabel label, T feature) where T : IFeature;
        public void RemoveFeatures<T>(ILabel label, Type featuresType = null) where T : IFeature;
        public IFeature GetFeature<T>(ILabel label, Type featureType = null) where T : IFeature;
        public IEnumerable<T> GetFeatures<T>(ILabel label, Type featuresType = null) where T : IFeature;
        public bool ContainsFeature<T>(ILabel label, Type featureType = null) where T : IFeature;
    }
}