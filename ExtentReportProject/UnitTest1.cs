using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumExtentTests
    {
        private IWebDriver _driver;
        private ExtentReports extent;
        private ExtentTest test;
        private Actions action;

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
            _driver = new ChromeDriver("D:\\Users\\AhmadRA\\source\\repos\\ExtentReportProject\\ExtentReportProject\\drivers\\");
            action = new Actions(_driver);
        }

        [Test]
        public void TestExample()
        {
            test = extent.CreateTest("TestExample With Action Wrapper").Info("Test Started");
            _driver.Navigate().GoToUrl("https://jqueryui.com/droppable/");
            test.Log(Status.Info, "Navigated to jqueryui.com/droppable/");

            // Switch to the frame containing the drag and drop elements
            _driver.SwitchTo().Frame(0);

            test.Log(Status.Info, "Prepare Source and Destination elemnt");
            var sourceElement = _driver.FindElement(By.Id("draggable"));
            var targetElement = _driver.FindElement(By.Id("droppable"));

            test.Log(Status.Info, "Drag and drop elemnt");
            //action.DragAndDrop(sourceElement, targetElement).Perform();

            action.MoveToElement(sourceElement)
                  .ClickAndHold()
                  .MoveToElement(targetElement)
                  .Release()
                  .Perform();

            // Step 3: Verify that the element was dropped successfully
            string dropText = targetElement.Text;
            //Assert.AreEqual("Dropped!", dropText, "The element was not dropped correctly.");
            Assert.That(dropText, Is.EqualTo("Dropped!"), "The element was not dropped correctly.");
            test.Log(Status.Pass, "Element Dropped successfully'");

            //Assert.IsTrue(_driver.Title.Contains("Swag"));
            //test.Log(Status.Pass, "Title contains 'Swag'");
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
            test.Log(Status.Info, "Browser closed");
        }

        [OneTimeTearDown]
        public void TearDownReporting()
        {
            extent.Flush();
        }
    }
}