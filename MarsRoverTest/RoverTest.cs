using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
namespace MarsRoverTest
{


    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void TestGetRoute()
        {
            RoverLogic logic = new RoverLogic();
            int[] coordinates = { 1, 2, 0 };
            string directions = "LMLMLMLMM";
            int[] borders = { 5, 5 };
            var testResult = logic.GetRoute(coordinates, directions, borders);
            int[] expected = {1, 3, 0 };
            Assert.AreEqual(expected.Length, testResult.Length);
               for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testResult[i]);
            }
        }
        [TestMethod]
        public void TestConvertResult()
        {
            RoverLogic logic = new RoverLogic();
            int[] coordinates = { 1, 3, 0 };
            var testResult = logic.ConvertToResult(coordinates);
            string expected = "1 3 N";
            Assert.AreEqual(expected, testResult);
        }
        [TestMethod]
        public void TestConvertToCoordinates()
        {
            RoverLogic logic = new RoverLogic();
            string input = "1 2 N";
            int[] testResult = logic.ConvertToCoordinates(input);
            int[] expected = { 1, 2, 0 };
            Assert.AreEqual(expected.Length, testResult.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testResult[i]);
            }
        }
        [TestMethod]
        public void TestCreateBorder()
        {
            RoverLogic logic = new RoverLogic();
            string input = "5 5";
            int[] testResult = logic.CreateBorder(input);
            int[] expected = { 5, 5 };
            Assert.AreEqual(expected.Length, testResult.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testResult[i]);
            }
        }
        [TestMethod]
        public void TestIsLimit()
        {
            RoverLogic logic = new RoverLogic();
            int[] coordinates = { 1, 5, 0 };
            int[] border = { 5, 5 };
            int[] testResult =logic.IsLimit(coordinates, border);
            int[] expected = { 0, 0, 0, 1 };
            Assert.AreEqual(expected.Length, testResult.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testResult[i]);
            }

        }

    }
}
