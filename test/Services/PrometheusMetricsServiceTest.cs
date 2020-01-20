using System;
using System.Threading.Tasks;
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using PipServices3.Components.Count;
using PipServices3.Components.Info;
using PipServices3.Prometheus.Count;
using Xunit;

namespace PipServices3.Prometheus.Services
{
    public class PrometheusMetricsServiceTest : IDisposable
    {
        private PrometheusMetricsService _service;
        private PrometheusCounters _counters;

        public PrometheusMetricsServiceTest()
        {
            var config = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", "3000"
            );
            _service = new PrometheusMetricsService();
            _service.Configure(config);

            var contextInfo = new ContextInfo();
            contextInfo.Name = "Test";
            contextInfo.Description = "This is a test container";

            _counters = new PrometheusCounters();

            var references = References.FromTuples(
                new Descriptor("pip-services3", "context-info", "default", "default", "1.0"), contextInfo,
                new Descriptor("pip-services3", "counters", "prometheus", "default", "1.0"), _counters,
                new Descriptor("pip-services3", "metrics-service", "prometheus", "default", "1.0"), _service
            );
            _service.SetReferences(references);


            _counters.OpenAsync(null).Wait();
            _service.OpenAsync(null).Wait();

            Task.Delay(500).Wait();
        }

        public void Dispose()
        {
            _service.CloseAsync(null).Wait();
            _counters.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestMetricsAsync()
        {
            _counters.IncrementOne("test.counter1");
            _counters.Stats("test.counter2", 2);
            _counters.Last("test.counter3", 3);
            _counters.TimestampNow("test.counter4");

            String status = await Invoke("/metrics");
            Assert.NotNull(status);
            Assert.True(status.Length > 0);
        }

        [Fact]
        public async Task TestMetricsAndResetAsync()
        {
            _counters.IncrementOne("test.counter1");
            _counters.Stats("test.counter2", 2);
            _counters.Last("test.counter3", 3);
            _counters.TimestampNow("test.counter4");

            String status = await Invoke("/metricsandreset");
            Assert.NotNull(status);
            Assert.True(status.Length > 0);

            var counter1 = _counters.Get("test.counter1", CounterType.Increment);
            var counter2 = _counters.Get("test.counter2", CounterType.Statistics);
            var counter3 = _counters.Get("test.counter3", CounterType.LastValue);
            var counter4 = _counters.Get("test.counter4", CounterType.Timestamp);

            Assert.Null(counter1.Count);
            Assert.Null(counter2.Count);
            Assert.Null(counter3.Last);
            Assert.Null(counter4.Time);
        }

        private static async Task<string> Invoke(string route)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:3000" + route);
                var responseValue = response.Content.ReadAsStringAsync().Result;
                return await Task.FromResult(responseValue);
            }
        }
    }
}
