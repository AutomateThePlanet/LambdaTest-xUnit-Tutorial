using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject.repeat
{
    [Serializable]
    public class RepeatTestCase : XunitTestCase
    {
        private int timesToRepeat;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public RepeatTestCase() { }

        public RepeatTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay testMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, int timesToRepeat)
            : base(diagnosticMessageSink, testMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments: null)
        {
            this.timesToRepeat = timesToRepeat;
        }

        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        object[] constructorArguments,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource)
        {
            var runCount = 0;

            while (true)
            {
                var delayedMessageBus = new DelayedMessageBus(messageBus);

                var summary = await base.RunAsync(diagnosticMessageSink, delayedMessageBus, constructorArguments, aggregator, cancellationTokenSource);
                if (++runCount >= timesToRepeat)
                {
                    delayedMessageBus.Dispose();
                    return summary;
                }

                diagnosticMessageSink.OnMessage(new DiagnosticMessage("Repeat test {0} number = '{1}'", DisplayName, runCount));
            }
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);

            data.AddValue("TimesToRepeat", timesToRepeat);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);

            timesToRepeat = data.GetValue<int>("TimesToRepeat");
        }
    }
}
