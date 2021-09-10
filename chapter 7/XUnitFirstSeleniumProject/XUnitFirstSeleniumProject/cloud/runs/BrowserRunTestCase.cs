using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject.cloud
{
    [Serializable]
    public class BrowserRunTestCase : LongLivedMarshalByRefObject, IXunitTestCase
    {
        public string DisplayName
        {
            get { return testCase.DisplayName; }
        }

        public string SkipReason
        {
            get { return testCase.SkipReason; }
        }

        public ISourceInformation SourceInformation
        {
            get { return testCase.SourceInformation; }
            set { testCase.SourceInformation = value; }
        }

        public ITestMethod TestMethod
        {
            get { return testCase.TestMethod; }
        }

        public object[] TestMethodArguments
        {
            get { return testCase.TestMethodArguments; }
        }

        public Dictionary<string, List<string>> Traits
        {
            get { return testCase.Traits; }
        }

        public string UniqueID
        {
            get { return testCase.UniqueID; }
        }

        public Exception InitializationException { get; set; }

        public int Timeout { get; set; }

        public IMethodInfo Method => throw new NotImplementedException();

        IXunitTestCase testCase;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public BrowserRunTestCase() { }

        public BrowserRunTestCase(IXunitTestCase testCase)
        {
            this.testCase = testCase;
        }

        // This method is called by the xUnit test framework classes to run the test case. We will do the
        // loop here, forwarding on to the implementation in XunitTestCase to do the heavy lifting. We will
        // continue to re-run the test until the aggregator has an error (meaning that some internal error
        // condition happened), or the test runs without failure, or we've hit the maximum number of tries.
        public Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                     IMessageBus messageBus,
                                     object[] constructorArguments,
                                     ExceptionAggregator aggregator,
                                     CancellationTokenSource cancellationTokenSource)
        {
            var delayedMessageBus = new DelayedMessageBus(messageBus);

            

            var summary = testCase.RunAsync(diagnosticMessageSink, messageBus, constructorArguments, aggregator, cancellationTokenSource);

            DriverFixture.Driver.Value?.ExecuteScript($"lambda-name={testCase.DisplayName}");

            if (aggregator.HasExceptions || summary.Result.Failed != 0)
            {
                
#if (!RELEASE)
                try
                {
                    DriverFixture.Driver.Value?.ExecuteScript("lambda-status=failed");
                    DriverFixture.Driver.Value?.ExecuteScript("lambda-exceptions", $"{aggregator.ToException()?.Message}{Environment.NewLine}{aggregator.ToException()?.StackTrace}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
#endif
                delayedMessageBus.Dispose();

                return summary;
            }
            else
            {
#if (!RELEASE)
                DriverFixture.Driver.Value?.ExecuteScript("lambda-status=passed");
#endif
            }

            ////DriverFixture.Driver.Value?.Dispose();

            return summary;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            testCase = info.GetValue<IXunitTestCase>("InnerTestCase");
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("InnerTestCase", testCase);
        }
    }
}
