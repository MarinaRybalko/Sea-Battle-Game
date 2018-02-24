
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameCore;
using GameService;

namespace UnitTest
{
    [TestClass]
    public class RandomAlgorithmTests
    {
        //[TestMethod]
        //public void Method1()
        //{
        //    var field = new Field();
        //    var count = 0;
        //    RandomFieldAlgorithm.NewArrangement(field);
        //    for (var i = 0; i < Field.Size; i++)
        //    {
        //        for (var j = 0; j < Field.Size; j++)
        //        {
        //            if (field.MatrixShips[i, j]) count++;
        //        }
        //    }

        //    Assert.AreEqual(20, count);
        //}
        //[TestMethod]
        //public void Method2()
        //{
        //    var methodInfo = typeof(RandomFieldAlgorithm).GetMethod("CountingValidCell", BindingFlags.NonPublic | BindingFlags.Static);           
        //    if (methodInfo != null)
        //        methodInfo.Invoke(null, new object[]
        //        {
        //           new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field())
        //       });
        //    var fieldInfo = typeof(RandomFieldAlgorithm).GetField("_validCell",
        //        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        //    if (fieldInfo != null) Assert.AreEqual(100, (int) fieldInfo.GetValue(null));
        //}
        //[TestMethod]
        //public void Method3()
        //{          
        //    var fieldInfo = typeof(RandomFieldAlgorithm).GetField("CompletingCell",
        //        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        //    if (fieldInfo != null)
        //    {
        //        var arr = (bool[,]) fieldInfo.GetValue(null);
        //        arr[0, 0] = true;
        //    }

        //    var check = RandomFieldAlgorithm.IsFreeLocation(new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field()),
        //        new Location(0, 0));
        //    Assert.AreEqual(false, check);
        //}
        //[TestMethod]
        //public void Metho4()
        //{
        //    var ship = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
        //    RandomFieldAlgorithm.AddShips(ship);
           
        //    Assert.AreEqual(1, RandomFieldAlgorithm.ListShips.Count);
        //}
        //[TestMethod]
        //public void Metho5()
        //{
        //    var ship = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
        //    var ship1 = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());
        //    var ship2 = new Ship(new Location(0, 0), Orientation.Horizontal, 1, new Field());

        //    RandomFieldAlgorithm.AddShips(ship);
        //    RandomFieldAlgorithm.AddShips(ship1);
        //    RandomFieldAlgorithm.AddShips(ship2);

        //    RandomFieldAlgorithm.RecalculationCompletingCell(ship);

        //    Assert.AreEqual(2, RandomFieldAlgorithm.ListShips.Count);
        //}
    }
}
