using NUnit.Framework;
using TDDDemoApp.Controllers;

namespace TDDDemoApp.UnitTest
{
    [TestFixture]
    public class ApplicationControllerFixture
    {
        class Info
        {
            [Test]
            public void Returns_Info_With_Title()
            {
                var testObject = new ApplicationController();

                var result = testObject.Info();

                Assert.That(result.Title, Is.EqualTo("TDD Demo"));
            }
        }

    }
}
