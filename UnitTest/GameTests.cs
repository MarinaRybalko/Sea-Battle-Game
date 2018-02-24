
using GameCore;
using GameService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{ 
//{
//    [TestClass]
//    public class GameTests
//    {
//        [TestMethod]
//        public void Method1()
//        {
//            var controller = new GameController();
//            controller.BeginGameMethod();
//            Assert.IsNotNull(controller.RightPlayer);
//            Assert.IsNotNull(controller.LeftPlayer.Oponent);
//            controller.LeftPlayer.CallTransferMove();
//            controller.RightPlayer.CallTransferMove();
//        }

//        [TestMethod]
//        public void Method2()
//        {
//            var controller = new GameController();
//            controller.RightPlayer = new MyBotPlayer(controller.RightField) {Oponent = controller.LeftPlayer};
//            controller.Init();
//            Assert.IsNotNull(controller.RightStatistics.CountLeftShot);
//            Assert.IsNotNull(controller.RightStatistics.CountShips);
//            Assert.IsNotNull(controller.LeftStatistics.CountShips);
//            Assert.IsNotNull(controller.LeftStatistics.CountLeftShot);

//        }

//        /// <summary>
//        /// CHANGE COUNT SHIP AFTER ALL
//        /// </summary>
//        [TestMethod]
//        public void Method3()
//        {
//            var controller = new GameController();
//            var endStatistics = new Statistics() {CountShips = 0, CountLeftShot = 0};
//            controller.ResetStatistics(endStatistics);
//            Assert.AreEqual(1, endStatistics.CountShips);
//            Assert.AreEqual(100, endStatistics.CountLeftShot);
//        }

//        [TestMethod]
//        public void Method4()
//        {
//            var controller = new GameController();
//            var human = new HumanPlay(new Field());
//            var RightPlayer = new MyBotPlayer(new Field()) {Oponent = human};
//            RightPlayer.TransferMove += controller.Transfer_Move;
//            RightPlayer.CallTransferMove();
//            Assert.AreEqual(true, human.CanMove);
//        }

//        [TestMethod]
//        public void Method5()
//        {
//            var controller = new GameController();

//            controller.RightPlayer = new MyBotPlayer(new Field());
//            controller.ResetStatistics(controller.RightStatistics);
//            controller.RightPlayer.OwnField.MadeShot += controller.Made_Shot;
//            controller.RightPlayer.OwnField.Shot(new Cell(0, 0));
//            var statistics = controller.RightStatistics;
//            Assert.AreEqual(99, statistics.CountLeftShot);
//        }

//        [TestMethod]
//        public void Method6()
//        {
//            var controller = new GameController();
//            var field = new Field();
//            controller.EnabledSwitch(field.CellField, true);
//            Assert.AreEqual(true, field.CellField[0, 0].Enabled);
//            controller.EnabledSwitch(field.CellField, false);
//            Assert.AreEqual(false, field.CellField[1, 1].Enabled);
//        }

//        [TestMethod]
//        public void Method7()
//        {
//            var controller = new GameController {RightPlayer = new MyBotPlayer(new Field())};
//            controller.GameOver(new Statistics() {CountShips = 0}, new Statistics() { CountShips = 1} );
//            Assert.AreEqual(1,controller.CountDefeat);
//            Assert.AreEqual(0, controller.CountWin);
//        }

//        [TestMethod]
//        public void Method8()
//        {
//            var controller = new GameController();
//            var field = new Field();
//            var count = 0;
//            controller.RandomArrangement( field);
//            for (var i = 0; i < Field.Size; i++)
//            {
//                for (var j = 0; j < Field.Size; j++)
//                {
//                    if (field.MatrixShips[i, j]) count++;
//                }
//            }
//            Assert.AreEqual(20,count);
//        }
//    }
}
