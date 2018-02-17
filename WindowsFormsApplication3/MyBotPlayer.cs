using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace SeaBattleGame
{
    enum MoveState { KnownDirection, UnknownShipLocation, UnknownDirection }

    enum Direction { Left, Top, Reight, Bottom }
      

    public class MyBotPlayer:Player
    {
        bool oneGuessing = false;

        Random random = new Random(DateTime.Now.Millisecond);
        protected bool[,] CheckShot = new bool[Field.Size, Field.Size];
        bool[] ChekDirection = new bool[4];

        int count;

        Location currentShot;
        Location firstShot;

        protected int IntactCell { get; set; }
        int intactDirection = 4;

        MoveState moveState = MoveState.UnknownShipLocation;
        Direction direction;

        CellStatus shotState;

        public MyBotPlayer(Field field):base(field)
        {
            shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(CheckShot);
            IntactCell = Field.Size * Field.Size;
        }

        public override void Move()
        {
            Thread.Sleep(500);
            if (Statistics.EndedGame) return;

            if (shotState == CellStatus.Drowned)
                MarkDrownedShip();

            CountingIntactCell();

            switch (moveState)
            {
                case MoveState.UnknownShipLocation:
                    LongShot();
                    break;

                case MoveState.UnknownDirection:
                    GuessingDirection();
                    break;

                case MoveState.KnownDirection:
                    SureShot();
                    break;
            }
            
            if (shotState == CellStatus.Miss)
                CallTransferMove();
        
        }

        protected virtual Ship MarkDrownedShip()
        {
           Ship ship = 
                oponentField.CellField[currentShot.I, currentShot.J].ShipIntoCell;

            ship.MarkShip(CheckShot);

            return ship;
        }

        protected virtual void CountingIntactCell()
        {
            IntactCell = 0;

            foreach (bool value in CheckShot) if(!value) IntactCell++;
        }

        void LongShot()
        {
            int shot = random.Next(0, IntactCell);

            Location newShot = OverrideShot(CheckShot, shot);

            shotState = oponentField.Shot(
                oponentField.CellField[newShot.I, newShot.J]);

            if (shotState == CellStatus.Crippled)
            {
                moveState = MoveState.UnknownDirection;
                oneGuessing = true;
                firstShot = newShot;
            }
            currentShot = newShot;

            if (shotState != CellStatus.Miss) Move();
        }

        protected virtual Location OverrideShot(bool[,] CheckShot, int shot)
        {
            return GeneralFunction.FromNumberToLocation(CheckShot, shot);
        }

        void GuessingDirection()
        {

            if (oneGuessing) CountingAllowableDirection();

            int shot = random.Next(0, intactDirection);
            count = 0;

            int numberDirection = -1;

            for (var i = 0; i < 4; i++)
            {
                    if (!ChekDirection[i])
                    {
                        if (count == shot)
                        {
                            ChekDirection[i] = true;
                            intactDirection--;
                            numberDirection = i;                 
                        }
                        count++;
                    }
            }

            CellStatus shotResult = ShotDirection((Direction)numberDirection);

            if (shotResult == CellStatus.Crippled)
            {
                direction = (Direction)numberDirection;
                moveState = MoveState.KnownDirection;
            }

            if (shotState != CellStatus.Miss) Move();
        }

        void CountingAllowableDirection()
        {
            for (var i = 0; i < ChekDirection.Length; i++) ChekDirection[i] = false;

            oneGuessing = false;
            intactDirection = 0;

            if ((currentShot.J > 0) && (!CheckShot[currentShot.I, currentShot.J - 1]))
            {
                 intactDirection++;               
            }
            else ChekDirection[0] = true;

            if ((currentShot.I > 0) && (!CheckShot[currentShot.I - 1, currentShot.J]))
            {
                intactDirection++;             
            }
            else ChekDirection[1] = true;

            if ((currentShot.J < Field.Size - 1) && (!CheckShot[currentShot.I, currentShot.J + 1]))
            {
               intactDirection++;             
            }
            else ChekDirection[2] = true;

            if ((currentShot.I < Field.Size - 1) && (!CheckShot[currentShot.I + 1, currentShot.J]))
            {
                intactDirection++;                
            }
            else ChekDirection[3] = true;
        }

        void SureShot()
        {
            int di = 0;
            int dj = 0;

            if (direction == Direction.Left) dj = -1;
            if (direction == Direction.Reight) dj = 1;
            if (direction == Direction.Top) di = -1;
            if (direction == Direction.Bottom) di = 1;

            CellStatus shotState;

            if ((GeneralFunction.PreventionIndexRange(currentShot.I + di, currentShot.J + dj))
                && (!CheckShot[currentShot.I + di, currentShot.J + dj]))
            {
                shotState = ShotDirection(direction);
            }
            else
            {
                ChangeDirection();
                shotState = ShotDirection(direction);
            }
            
            if (shotState == CellStatus.Miss) ChangeDirection();
            else Move();
        }

        void ChangeDirection()
        {
            if (direction == Direction.Left) direction = Direction.Reight;
            else
            if (direction == Direction.Reight) direction = Direction.Left;

            if (direction == Direction.Top) direction = Direction.Bottom;
            else
            if (direction == Direction.Bottom) direction = Direction.Top;

            currentShot = firstShot;
        }

        CellStatus ShotDirection(Direction direciton)
        {
            int di = 0;
            int dj = 0;

            if (direciton == Direction.Left)
            {
                dj = -1; di = 0;
            }

            if (direciton == Direction.Top)
            {
                dj = 0; di = -1;
            }

            if (direciton == Direction.Reight)
            {
                dj = 1; di = 0;
            }

            if (direciton == Direction.Bottom)
            {
                dj = 0; di = 1;
            }
           
            shotState = oponentField.Shot(
                oponentField.CellField[currentShot.I + di, currentShot.J + dj]);
            CheckShot[currentShot.I + di, currentShot.J + dj] = true;


            if (shotState != CellStatus.Miss)
            {
                currentShot.I += di;
                currentShot.J += dj;
            }

            if (shotState == CellStatus.Drowned)
                moveState = MoveState.UnknownShipLocation;

            return shotState;
        }

    }
}
