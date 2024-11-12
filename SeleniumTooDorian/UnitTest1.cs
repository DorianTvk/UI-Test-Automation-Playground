using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace UITestingPlayground.Tests
{
    public class UITests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.uitestingplayground.com/");
        }

        [Test]
        public void TestDynamicID()
        {
            driver.FindElement(By.LinkText("Dynamic ID")).Click();
            var dynamicIdElement = driver.FindElement(By.XPath("//button[contains(text(),'Button with Dynamic ID')]"));
            Assert.IsTrue(dynamicIdElement.Displayed);
        }

        [Test]
        public void TestHiddenLayers()
        {
            driver.FindElement(By.LinkText("Hidden Layers")).Click();
            var button = driver.FindElement(By.Id("layer1"));
            button.Click();
            var layerMessage = driver.FindElement(By.Id("layer1")).Text;
            Assert.AreEqual("Layer 1", layerMessage);
        }

        [Test]
        public void TestButtonClick()
        {
            driver.FindElement(By.LinkText("Click")).Click();
            driver.FindElement(By.Id("click")).Click();
            var message = driver.FindElement(By.Id("message")).Text;
            Assert.AreEqual("Clicked!", message);
        }

        [Test]
        public void TestTextInput()
        {
            driver.FindElement(By.LinkText("Text Input")).Click();
            var inputField = driver.FindElement(By.Id("newInput"));
            inputField.SendKeys("Test input");
            Assert.AreEqual("Test input", inputField.GetAttribute("value"));
        }

        [Test]
        public void TestMouseOver()
        {
            driver.FindElement(By.LinkText("Mouse Over")).Click();
            var hoverElement = driver.FindElement(By.Id("mouseOver"));
            var actions = new Actions(driver);
            actions.MoveToElement(hoverElement).Perform();

            // Kontrolli, kas hoveri efekt töötab (näiteks, et element muutub nähtavaks)
            var tooltip = driver.FindElement(By.Id("tooltip"));
            Assert.IsTrue(tooltip.Displayed);
        }

        [Test]
        public void TestAlert()
        {
            driver.FindElement(By.LinkText("Alert")).Click();
            var alertButton = driver.FindElement(By.Id("alertButton"));
            alertButton.Click();

            // Oota, kuni hoiat