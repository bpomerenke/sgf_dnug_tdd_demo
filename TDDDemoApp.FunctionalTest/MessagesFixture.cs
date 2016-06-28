using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;

namespace TDDDemoApp.FunctionalTest
{
    [TestFixture]
    public class MessagesFixture
    {
        [Test]
        public void Home_Page_Shows_Existing_Messages()
        {
            DatabaseHelper.ExecuteCommand("DELETE FROM Messages");
            DatabaseHelper.ExecuteCommand("INSERT INTO Messages (Message) VALUES('foo'),('Bar')");

            using (var driver = new EventFiringWebDriver(new ChromeDriver()))
            {
                driver.Navigate().GoToUrl("http://localhost/TDDDemoApp/");

                var wait = new WebDriverWait(driver, new TimeSpan(0,0,30));
                wait.Until(x=>!x.FindElement(By.Id("message-loading")).Displayed);

                var messages = driver.FindElements(By.TagName("blockquote"));
                Assert.That(messages.Count, Is.EqualTo(2));

                Thread.Sleep(2000);
            }
        }
    }
}
