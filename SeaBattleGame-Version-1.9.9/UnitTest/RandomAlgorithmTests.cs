
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameCore;
using GameService;
using Orientation = GameCore.Orientation;

namespace UnitTest
{
    [TestClass]
    public class RandomAlgorithmTests
    {
        [TestMethod]
        public void NewArrangementTest()
        {
            var field = new Field();
            var count = 0;
            field.RandomShips = new RandomFieldAlgorithm(field);
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (field.MatrixShips[i, j]) count++;
                }
            }

            Assert.AreEqual(20, count);
        }

        [TestMethod]
        public void CountingValidCellTest()
        {
            var rn = new RandomFieldAlgorithm(new Field());
            var methodInfo =
                typeof(RandomFieldAlgorithm).GetMethod("CountingValidCell",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo != null)
                methodInfo.Invoke(rn, new object[]
                {
                    new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field())
                });
            var fieldInfo = typeof(RandomFieldAlgorithm).GetField("_validCell",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            if (fieldInfo != null) Assert.AreEqual(100, (int) fieldInfo.GetValue(rn));
        }

        [TestMethod]
        public void CompletingCellTest()
        {
            var rn = new RandomFieldAlgorithm(new Field());
            var fieldInfo = typeof(RandomFieldAlgorithm).GetField("CompletingCell",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            if (fieldInfo != null)
            {
                var arr = (bool[,]) fieldInfo.GetValue(rn);
                arr[0, 0] = true;
            }

            var check = rn.IsFreeLocation(
                new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field()),
                new Location(0, 0));
            Assert.AreEqual(false, check);
        }

        [TestMethod]
        public void AddShipsTest()
        {
            var rn = new RandomFieldAlgorithm(new Field());
            rn.ListShips.Clear();
            var ship = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
            rn.AddShips(ship);

            Assert.AreEqual(1, rn.ListShips.Count);
        }

        [TestMethod]
        public void RecalculationCompletingCellTest()
        {
 
            var rn = new RandomFieldAlgorithm(new Field());
            rn.ListShips.Clear();
            var ship = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
            var ship1 = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
            var ship2 = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());

            rn.AddShips(ship);
            rn.AddShips(ship1);
            rn.AddShips(ship2);
            rn.RecalculationCompletingCell(ship);

            Assert.AreEqual(2, rn.ListShips.Count);
        }
    }
}
