using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumExtentTests
    {
        private IWebDriver driver;
        private ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            var sparkReporter = new ExtentSparkReporter("extentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver("D:\\Users\\AhmadRA\\source\\repos\\ExtentReportProject\\ExtentReportProject\\drivers\\");
        }

        [Test]
        public void TestExample()
        {
            test = extent.CreateTest("TestExample").Info("Test Started");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            test.Log(Status.Info, "Navigated to www.saucedemo.com");

            Assert.IsTrue(driver.Title.Contains("Swag"));
            test.Log(Status.Pass, "Title contains 'Swag'");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
            test.Log(Status.Info, "Browser closed");
        }

        [OneTimeTearDown]
        public void TearDownReporting()
        {
            extent.Flush();
        }
    }
}