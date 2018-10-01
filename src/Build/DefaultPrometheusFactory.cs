using System;
using PipServices.Components.Build;
using PipServices.Commons.Refer;
using PipServices.Prometheus.Count;
using PipServices.Prometheus.Services;

namespace PipServices.Prometheus.Build
{
    /// <summary>
    /// Creates Prometheus components by their descriptors.
    /// </summary>
    /// See <see cref="Factory"/>, <see cref="PrometheusCounters"/>, <see cref="PrometheusMetricsService"/>
    public class DefaultPrometheusFactory: Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services", "factory", "prometheus", "default", "1.0");
        public static readonly Descriptor PrometheusCountersDescriptor = new Descriptor("pip-services", "counters", "prometheus", "*", "1.0");
        public static readonly Descriptor PrometheusMetricsServiceDescriptor = new Descriptor("pip-services", "metrics-service", "prometheus", "*", "1.0");

        /// <summary>
        /// Create a new instance of the factory.
        /// </summary>
        public DefaultPrometheusFactory()
        {
            RegisterAsType(DefaultPrometheusFactory.PrometheusCountersDescriptor, typeof(PrometheusCounters));
            RegisterAsType(DefaultPrometheusFactory.PrometheusMetricsServiceDescriptor, typeof(PrometheusMetricsService));
        }
    }
}
