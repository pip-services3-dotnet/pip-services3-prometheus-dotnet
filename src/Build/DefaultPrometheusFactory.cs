using System;
using PipServices3.Components.Build;
using PipServices3.Commons.Refer;
using PipServices3.Prometheus.Count;
using PipServices3.Prometheus.Services;

namespace PipServices3.Prometheus.Build
{
    /// <summary>
    /// Creates Prometheus components by their descriptors.
    /// </summary>
    /// See <a href="https://pip-services3-dotnet.github.io/pip-services3-components-dotnet/class_pip_services_1_1_components_1_1_build_1_1_factory.html">Factory</a>, 
    /// <a href="https://pip-services3-dotnet.github.io/pip-services3-prometheus-dotnet/class_pip_services_1_1_prometheus_1_1_count_1_1_prometheus_counters.html">PrometheusCounters</a>, 
    /// <a href="https://pip-services3-dotnet.github.io/pip-services3-prometheus-dotnet/class_pip_services_1_1_prometheus_1_1_services_1_1_prometheus_metrics_service.html">PrometheusMetricsService</a>
    public class DefaultPrometheusFactory: Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services", "factory", "prometheus", "default", "1.0");
        public static readonly Descriptor Descriptor3 = new Descriptor("pip-services3", "factory", "prometheus", "default", "1.0");
        public static readonly Descriptor PrometheusCountersDescriptor = new Descriptor("pip-services", "counters", "prometheus", "*", "1.0");
        public static readonly Descriptor PrometheusCounters3Descriptor = new Descriptor("pip-services3", "counters", "prometheus", "*", "1.0");
        public static readonly Descriptor PrometheusMetricsServiceDescriptor = new Descriptor("pip-services", "metrics-service", "prometheus", "*", "1.0");
        public static readonly Descriptor PrometheusMetricsService3Descriptor = new Descriptor("pip-services3", "metrics-service", "prometheus", "*", "1.0");

        /// <summary>
        /// Create a new instance of the factory.
        /// </summary>
        public DefaultPrometheusFactory()
        {
            RegisterAsType(DefaultPrometheusFactory.PrometheusCountersDescriptor, typeof(PrometheusCounters));
            RegisterAsType(DefaultPrometheusFactory.PrometheusCounters3Descriptor, typeof(PrometheusCounters));
            RegisterAsType(DefaultPrometheusFactory.PrometheusMetricsServiceDescriptor, typeof(PrometheusMetricsService));
            RegisterAsType(DefaultPrometheusFactory.PrometheusMetricsService3Descriptor, typeof(PrometheusMetricsService));
        }
    }
}
