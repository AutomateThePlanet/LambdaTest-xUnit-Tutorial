using System.Collections.Generic;

namespace XUnitFirstSeleniumProject.cloud.settings
{
    public class ExecutionSettings
    {
        public bool RunInCloud { get; set; }
        public string DefaultBrowser { get; set; }
        public string BrowserVersion { get; set; }
        public string PlatformName { get; set; }
        public List<Dictionary<string, string>> Arguments { get; set; }
    }
}
