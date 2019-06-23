using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleGame1.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame1.Logic.Model.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void UserTest1()
        {
            string Login = Guid.NewGuid().ToString();
            DateTime Date = DateTime.Now.AddYears(-18);
            string password = Guid.NewGuid().ToString();
            User user = new User(Login, password, Date);
            Assert.AreEqual(Login, user.Login);
            Assert.AreEqual(Date, user.DateOfBirth);
        }

        [TestMethod()]
        public void CheckPassTest()
        {
            string password = Guid.NewGuid().ToString();
            var getHash1 = User.GetHashCode(password);
            var getHash2 = User.GetHashCode(password);
            Assert.AreEqual(getHash1, getHash2);
            Assert.AreNotEqual(password, getHash1);
            Assert.AreNotEqual(getHash1, User.GetHashCode(password + 1));
        }

        [TestMethod()]
        public void ChoseMenuTest()
        {
            Element[,] elements = new Element[5,5];
            for(int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLongLength(1); j++)
                {
                    elements[i,j] = new Element(Guid.NewGuid().ToString());
                }
            }
            ChoseMenu TestMenu = new ChoseMenu(elements);
            int k = 0;
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLongLength(1); j++)
                {
                    if (elements[i, j].IsSelected)
                    {
                        k++;
                    }
                    if (k > 1)
                    {
                        Assert.Fail();
                    }
                }
            }
        }
    }
}