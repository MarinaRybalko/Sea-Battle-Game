using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameService;
using GameCore;

namespace UnitTest
{
    [TestClass]
    public class MyBotPlayerTests
    {

        //    [TestMethod]
        //    public void TestMethod2()
        //    {
        //        var player= new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("ChangeDirection", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (methodInfo != null) methodInfo.Invoke(player, new object[] {Direction.Top});
        //        var fieldInfo =
        //            typeof(MyBotPlayer).GetField("_direction", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (fieldInfo != null) Assert.AreEqual( Direction.Bottom, (Direction)fieldInfo.GetValue(player));
        //    }
        //    [TestMethod]
        //    public void TestMethod3()
        //    {
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("CountingIntactCell", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (methodInfo != null) methodInfo.Invoke(player, null);
        //        var fieldInfo =
        //            typeof(MyBotPlayer).GetField("IntactCell", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (fieldInfo != null) Assert.AreEqual( 0, (int)fieldInfo.GetValue(player));
        //    }
        //    [TestMethod]
        //    public void TestMethod4()
        //    {
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("OverrideShot", BindingFlags.Instance | BindingFlags.NonPublic);
        //        var matrix = new bool[Field.Size, Field.Size];
        //        matrix.Initialize();
        //        Location? location=null;
        //        if (methodInfo != null)
        //        { location =(Location) methodInfo.Invoke(player, new object[] {matrix,2});}            
        //        Assert.AreEqual(new Location(0, 2),location);
        //    }
        //    [TestMethod]
        //    public void TestMethod5()
        //    {
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("CountingAllowableDirection", BindingFlags.Instance | BindingFlags.NonPublic);
        //        var matrix = new bool[Field.Size, Field.Size];
        //        matrix.Initialize();
        //        if (methodInfo != null)
        //        { methodInfo.Invoke(player, null); }
        //        var fieldInfo =
        //            typeof(MyBotPlayer).GetField("_chekDirection", BindingFlags.Instance | BindingFlags.NonPublic);
        //        bool[]arr= new bool[Enum.GetValues(typeof(Direction)).Length];
        //        if (fieldInfo != null)
        //        {
        //            arr = (bool[]) fieldInfo.GetValue(player);
        //        }

        //        if (fieldInfo == null) return;
        //        Assert.AreEqual(true, arr[0]);
        //        Assert.AreEqual(true, arr[1]);
        //        Assert.AreNotEqual( true, arr[2]);
        //        Assert.AreNotEqual( true, arr[3]);
        //    }
        //    [TestMethod]
        //    public void TestMethod6()
        //    {
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("ChangeDirection", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (methodInfo != null) methodInfo.Invoke(player, new object[] { Direction.Top });
        //        var fieldInfo =
        //            typeof(MyBotPlayer).GetField("_direction", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (fieldInfo != null) Assert.AreEqual( Direction.Bottom, (Direction)fieldInfo.GetValue(player));
        //    }
        //    [TestMethod]
        //    public void TestMethod7()
        //    {      
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("ShotDirection", BindingFlags.Instance | BindingFlags.NonPublic);


        //        var status = CellStatus.Empty;

        //        if (methodInfo != null)
        //        {
        //            status = (CellStatus)methodInfo.Invoke(player,
        //                new object[]
        //                {
        //                    Direction.Bottom,
        //                    new Field(), 

        //                });
        //        }
        //        Assert.AreEqual(CellStatus.Miss, status);
        //    }
        //    [TestMethod]
        //    public void TestMethod8()
        //    {
        //        var player = new MyBotPlayer(new Field());
        //        var methodInfo =
        //            typeof(MyBotPlayer).GetMethod("SureShot", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (methodInfo != null) methodInfo.Invoke(player, new object[] { Direction.Bottom, new Field(),  });
        //        var fieldInfo =
        //          typeof(MyBotPlayer).GetField("shotState", BindingFlags.Instance | BindingFlags.NonPublic);
        //       if (fieldInfo != null) Assert.AreEqual( CellStatus.Miss, (CellStatus)fieldInfo.GetValue(player));
        //    }
        //}
    }
}
