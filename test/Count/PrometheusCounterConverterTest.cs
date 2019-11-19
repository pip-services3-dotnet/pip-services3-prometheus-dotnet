using System;
using System.Collections.Generic;
using PipServices3.Components.Count;
using Xunit;

namespace PipServices3.Prometheus.Count
{
    public sealed class PrometheusCounterConverterTest
    {
        [Theory]
        [InlineData("MyService1.MyCommand1.exec_count", "exec_count")]
        [InlineData("MyService1.MyCommand1.exec_time", "exec_time")]
        [InlineData("MyService1.MyCommand1.exec_errors", "exec_errors")]
        public void KnownCounter_Exec_ServiceMetrics_Good(string counterName, string expectedName)
        {
            var counters = new List<Counter>
            {
                new Counter(counterName , CounterType.Increment) { Count = 1, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Interval) { Count = 11, Max = 13, Min = 3, Average = 3.5, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.LastValue) { Last = 2, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Statistics) { Count = 111, Max = 113, Min = 13, Average = 13.5, Time = DateTime.MinValue }
            };

            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");

            var expected = $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 1\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 13\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 3\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 3.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 11\n"
                + $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 2\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 113\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 13\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 13.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"}} 111\n";

            Assert.Equal(expected, body);
        }

        [Theory]
        [InlineData("MyTarget1.MyService1.MyCommand1.call_count", "call_count")]
        [InlineData("MyTarget1.MyService1.MyCommand1.call_time", "call_time")]
        [InlineData("MyTarget1.MyService1.MyCommand1.call_errors", "call_errors")]
        public void KnownCounter_Exec_ClientMetrics_Good(string counterName, string expectedName)
        {
            var counters = new List<Counter>
            {
                new Counter(counterName , CounterType.Increment) { Count = 1, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Interval) { Count = 11, Max = 13, Min = 3, Average = 3.5, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.LastValue) { Last = 2, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Statistics) { Count = 111, Max = 113, Min = 13, Average = 13.5, Time = DateTime.MinValue }
            };

            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");

            var expected = $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 1\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 13\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 3\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 3.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 11\n"
                + $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 2\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 113\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 13\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 13.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\",target=\"MyTarget1\"}} 111\n";

            Assert.Equal(expected, body);
        }

        [Theory]
        [InlineData("queue.default.sent_messages", "queue_sent_messages")]
        [InlineData("queue.default.received_messages", "queue_received_messages")]
        [InlineData("queue.default.dead_messages", "queue_dead_messages")]
        public void KnownCounter_Exec_QueueMetrics_Good(string counterName, string expectedName)
        {
            var counters = new List<Counter>
            {
                new Counter(counterName , CounterType.Increment) { Count = 1, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Interval) { Count = 11, Max = 13, Min = 3, Average = 3.5, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.LastValue) { Last = 2, Time = DateTime.MinValue },
                new Counter(counterName , CounterType.Statistics) { Count = 111, Max = 113, Min = 13, Average = 13.5, Time = DateTime.MinValue }
            };

            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");

            var expected = $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 1\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 13\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 3\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 3.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 11\n"
                + $"# TYPE {expectedName} gauge\n{expectedName}{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 2\n"
                + $"# TYPE {expectedName}_max gauge\n{expectedName}_max{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 113\n"
                + $"# TYPE {expectedName}_min gauge\n{expectedName}_min{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 13\n"
                + $"# TYPE {expectedName}_average gauge\n{expectedName}_average{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 13.5\n"
                + $"# TYPE {expectedName}_count gauge\n{expectedName}_count{{source=\"MyApp\",instance=\"MyInstance\",queue=\"default\"}} 111\n";

            Assert.Equal(expected, body);
        }

        [Fact]
        public void EmptyCounters()
        {
            var counters = new List<Counter>();
            var body = PrometheusCounterConverter.ToString(counters, string.Empty, string.Empty);
            Assert.Equal(string.Empty, body);
        }

        [Fact]
        public void NullValues()
        {
            var body = PrometheusCounterConverter.ToString(null, null, null);
            Assert.Equal(string.Empty, body);
        }

        [Fact]
        public void SingleIncrement_NoLabels()
        {
            var counters = new List<Counter>
            {
                new Counter("MyCounter", CounterType.Increment)
                {
                    Average = 2,
                    Min = 1,
                    Max = 3,
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, null, null);
            const string expected = "# TYPE mycounter gauge\nmycounter 2\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void SingleIncrement_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyCounter", CounterType.Increment)
                {
                    Average = 2,
                    Min = 1,
                    Max = 3,
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");
            const string expected = "# TYPE mycounter gauge\nmycounter{source=\"MyApp\",instance=\"MyInstance\"} 2\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiIncrement_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyCounter1", CounterType.Increment)
                {
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyCounter2", CounterType.Increment)
                {
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");
            const string expected = "# TYPE mycounter1 gauge\nmycounter1{source=\"MyApp\",instance=\"MyInstance\"} 2\n"
                                  + "# TYPE mycounter2 gauge\nmycounter2{source=\"MyApp\",instance=\"MyInstance\"} 5\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiIncrement_ExecWithOnlyTwo_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyCounter1.exec_time", CounterType.Increment)
                {
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyCounter2.exec_time", CounterType.Increment)
                {
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");
            const string expected = "# TYPE mycounter1_exec_time gauge\nmycounter1_exec_time{source=\"MyApp\",instance=\"MyInstance\"} 2\n"
                                    + "# TYPE mycounter2_exec_time gauge\nmycounter2_exec_time{source=\"MyApp\",instance=\"MyInstance\"} 5\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiIncrement_Exec_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyService1.MyCommand1.exec_time", CounterType.Increment)
                {
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyService2.MyCommand2.exec_time", CounterType.Increment)
                {
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");
            const string expected = "# TYPE exec_time gauge\nexec_time{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 2\n"
                                  + "# TYPE exec_time gauge\nexec_time{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 5\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiInterval_Exec_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyService1.MyCommand1.exec_time", CounterType.Interval)
                {
                    Min = 1,
                    Max = 3,
                    Average = 2,
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyService2.MyCommand2.exec_time", CounterType.Interval)
                {
                    Min = 2,
                    Max = 4,
                    Average = 3,
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");

            const string expected =
                "# TYPE exec_time_max gauge\nexec_time_max{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 3\n"
                + "# TYPE exec_time_min gauge\nexec_time_min{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 1\n"
                + "# TYPE exec_time_average gauge\nexec_time_average{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 2\n"
                + "# TYPE exec_time_count gauge\nexec_time_count{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 2\n"
                + "# TYPE exec_time_max gauge\nexec_time_max{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 4\n"
                + "# TYPE exec_time_min gauge\nexec_time_min{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 2\n"
                + "# TYPE exec_time_average gauge\nexec_time_average{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 3\n"
                + "# TYPE exec_time_count gauge\nexec_time_count{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 5\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiStatistics_Exec_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyService1.MyCommand1.exec_time", CounterType.Statistics)
                {
                    Min = 1,
                    Max = 3,
                    Average = 2,
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyService2.MyCommand2.exec_time", CounterType.Statistics)
                {
                    Min = 2,
                    Max = 4,
                    Average = 3,
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");

            const string expected =
                "# TYPE exec_time_max gauge\nexec_time_max{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 3\n"
                + "# TYPE exec_time_min gauge\nexec_time_min{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 1\n"
                + "# TYPE exec_time_average gauge\nexec_time_average{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 2\n"
                + "# TYPE exec_time_count gauge\nexec_time_count{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 2\n"
                + "# TYPE exec_time_max gauge\nexec_time_max{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 4\n"
                + "# TYPE exec_time_min gauge\nexec_time_min{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 2\n"
                + "# TYPE exec_time_average gauge\nexec_time_average{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 3\n"
                + "# TYPE exec_time_count gauge\nexec_time_count{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 5\n";
            Assert.Equal(expected, body);
        }

        [Fact]
        public void MultiLastValue_Exec_SourceInstance()
        {
            var counters = new List<Counter>
            {
                new Counter("MyService1.MyCommand1.exec_time", CounterType.LastValue)
                {
                    Count = 2,
                    Last = 3,
                    Time = DateTime.MinValue
                },
                new Counter("MyService2.MyCommand2.exec_time", CounterType.LastValue)
                {
                    Count = 5,
                    Last = 10,
                    Time = DateTime.MinValue
                }
            };
            var body = PrometheusCounterConverter.ToString(counters, "MyApp", "MyInstance");
            const string expected = "# TYPE exec_time gauge\nexec_time{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService1\",command=\"MyCommand1\"} 3\n"
                                    + "# TYPE exec_time gauge\nexec_time{source=\"MyApp\",instance=\"MyInstance\",service=\"MyService2\",command=\"MyCommand2\"} 10\n";
            Assert.Equal(expected, body);
        }
    }
}
