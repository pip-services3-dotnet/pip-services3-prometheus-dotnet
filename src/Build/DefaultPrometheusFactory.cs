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
    /// See <a href="https://rawgit.com/pip-services-dotnet/pip-services-components-dotnet/master/doc/api/class_pip_services_1_1_components_1_1_build_1_1_factory.html">Factory</a>, 
    /// <a href="https://rawgit.com/pip-services-dotnet/pip-services-prometheus-dotnet/master/doc/api/class_pip_services_1_1_prometheus_1_1_count_1_1_prometheus_counters.html">PrometheusCounters</a>, 
    /// <a href="https://rawgit.com/pip-services-dotnet/pip-services-prometheus-dotnet/master/doc/api/class_pip_services_1_1_prometheus_1_1_services_1_1_prometheus_metrics_service.html">PrometheusMetricsService</a>
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
