
using GameCore;
using GameService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{ 
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GetNormalPlayerTest()
        {
            var controller = new GameController();
            controller.GetNormalPlayer();
            Assert.IsNotNull(controller.RightPlayer);
            Assert.IsNotNull(controller.LeftPlayer.Oponent);
            Assert.IsNotNull(controller.RightPlayer.Oponent);

        }
       
        [TestMethod]
        public void InitTest()
        {
            var controller = new GameController();
            controller.RightPlayer = new MyBotPlayer(controller.RightField) { Oponent = controller.LeftPlayer };
            controller.Init();
            Assert.IsNotNull(controller.RightStatistics.CountLeftShot);
            Assert.IsNotNull(controller.RightStatistics.CountShips);
            Assert.IsNotNull(controller.LeftStatistics.CountShips);
            Assert.IsNotNull(controller.LeftStatistics.CountLeftShot);

        }      
        [TestMethod]
        public void ResetStatisticsTest()
        {
            var controller = new GameController();
            var endStatistics = new Statistics() { CountShips = 0, CountLeftShot = 0 };
            controller.ResetStatistics(endStatistics);
            Assert.AreEqual(10, endStatistics.CountShips);
            Assert.AreEqual(100, endStatistics.CountLeftShot);
        }

        [TestMethod]
        public void TransfetMoveTest()
        {
            var controller = new GameController();
            var human = new HumanPlay(new Field());
            var rightPlayer = new MyBotPlayer(new Field()) { Oponent = human };
            rightPlayer.TransferMove += controller.Transfer_Move;
            rightPlayer.CallTransferMove();
            Assert.AreEqual(true, human.CanMove);
        }

        [TestMethod]
        public void Made_ShotTest()
        {
            var controller = new GameController();
            controller.RightPlayer = new MyBotPlayer(new Field());
            controller.ResetStatistics(controller.LeftStatistics);
            controller.RightPlayer.OwnField.MadeShot += controller.MadeShot;
            controller.RightPlayer.OwnField.Shot(new Cell(0, 0));
            var statistics = controller.LeftStatistics;
            Assert.AreEqual(99, statistics.CountLeftShot);
        }

        [TestMethod]
        public void EnabledSwitchTest()
        {
            var controller = new GameController();
            var field = new Field();
            controller.EnabledSwitch(field.CellField, true);
            Assert.AreEqual(true, field.CellField[0, 0].Enabled);
            controller.EnabledSwitch(field.CellField, false);
            Assert.AreEqual(false, field.CellField[1, 1].Enabled);
        }

        [TestMethod]
        public void GameOverTest()
        {
            var controller = new GameController { RightPlayer = new MyBotPlayer(new Field()) };
            controller.GameOver(new Statistics() { CountShips = 0 }, new Statistics() { CountShips = 1 });
            Assert.AreEqual(1, controller.CountDefeat);
            Assert.AreEqual(0, controller.CountWin);
        }

        [TestMethod]
        public void RandomArrangementTest()
        {
            var controller = new GameController();
            var field = new Field();
            var count = 0;
            field.RandomShips = new RandomFieldAlgorithm(field);
            controller.RandomArrangement(field);
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (field.MatrixShips[i, j]) count++;
                }
            }
            Assert.AreEqual(20, count);
        }
    }
}
