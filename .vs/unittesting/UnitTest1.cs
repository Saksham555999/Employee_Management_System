using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BLOGIC;
namespace unittesting
{
    [TestClass]
    public class UnitTest1
    {
        blogic obj = new blogic();
        [TestMethod]
        
        public void Login()
        {
            
            object result = obj.EmployeeLogin("EMP2", "india");
            
            Assert.AreEqual(1,result);
        }
    }
}
