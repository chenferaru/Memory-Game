using System.Collections.Generic;

namespace MemoryGameEngine
{
    public struct ComputerPlayer<T>
    {
        private int m_NumOfPairs;
        private List<BrainCell> m_Brain;
        private eComputerPlayerLevel m_ComputerLevel;

        public enum eComputerPlayerLevel
        {
            Easy,
            Medium,
            Hard
        }

        public eComputerPlayerLevel ComputerLevel
        {
            get { return m_ComputerLevel; }
            set { m_ComputerLevel = value; }
        }

        public ComputerPlayer(int i_Difficulty)
        {
            m_NumOfPairs = 0;
            m_Brain = new List<BrainCell>();
            switch (i_Difficulty)
            {
                case 1:
                    m_ComputerLevel = eComputerPlayerLevel.Easy;
                    break;
                case 2:
                    m_ComputerLevel = eComputerPlayerLevel.Medium;
                    break;
                case 3:
                    m_ComputerLevel = eComputerPlayerLevel.Hard;
                    break;
                default:
                    m_ComputerLevel = eComputerPlayerLevel.Medium;
                    break;
            }
        }

        public struct BrainCell
        {
            private SpotOnBoard m_spotOnBoard;
            private T m_CardContent;

            public BrainCell(SpotOnBoard i_SpotOnBoard, T i_CardContent)
            {
                m_spotOnBoard = i_SpotOnBoard;
                m_CardContent = i_CardContent;
            }
            public SpotOnBoard SpotOnBoard
            {
                get
                {
                    return m_spotOnBoard;
                }

                set
                {
                    m_spotOnBoard = value;
                }
            }

            public T CardContent
            {
                get
                {
                    return m_CardContent;
                }

                set
                {
                    m_CardContent = value;
                }
            }
        }

        public void AddBrainCell(SpotOnBoard i_SpotOnBoard, T i_content)
        {
            bool alreadyInBrain = false;

            foreach (BrainCell cell in m_Brain)
            {
                if (i_SpotOnBoard.IsEqual(cell.SpotOnBoard))
                {
                    alreadyInBrain = true;
                    break;
                }
            }

            if (!alreadyInBrain)
            {
                BrainCell newCell = new BrainCell(i_SpotOnBoard, i_content);
                m_Brain.Add(newCell);
            }
        }

        public void DeleteBrainCell(T i_content)
        {
            m_Brain.RemoveAll(brainCell => brainCell.CardContent.Equals(i_content));
        }

        public SpotOnBoard FindPair(SpotOnBoard i_SpotOnBoard, Board<T> i_Board)
        {
            SpotOnBoard spotOnBoard = new SpotOnBoard();
            bool foundPair = false;
            T content = i_Board.CardContent(i_SpotOnBoard.Row, i_SpotOnBoard.Col);

            foreach (BrainCell cell in m_Brain)
            {
                if (content.Equals(cell.CardContent) && !cell.SpotOnBoard.IsEqual(i_SpotOnBoard))
                {
                    spotOnBoard = cell.SpotOnBoard;
                    foundPair = true;
                    m_Brain.Remove(cell);
                    break;
                }
            }

            if (!foundPair)
            {
                spotOnBoard = i_Board.GenerateRandomUnflippedSpotOnBoard();
            }

            return spotOnBoard;
        }

        public int NumOfPairs
        {
            get
            {
                return m_NumOfPairs;
            }
            set
            {
                m_NumOfPairs = value;
            }
        }

        public List<BrainCell> Brain
        {
            get
            {
                return m_Brain;
            }

            set
            {
                m_Brain = value;
            }
        }
        public bool Turn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot, Board<T> io_Board)
        {
            T firstSpotContent;
            T secondSpotContent;
            bool foundPair;

            firstSpotContent = io_Board.CardContent(i_FirstSpot.Row, i_FirstSpot.Col);
            secondSpotContent = io_Board.CardContent(i_SecondSpot.Row, i_SecondSpot.Col);
            if (firstSpotContent.Equals(secondSpotContent))
            {
                m_NumOfPairs++;
                io_Board.NumOfPairsLeftInBoard--;
                foundPair = true;
            }
            else
            {
                foundPair = false;
                if (m_ComputerLevel != eComputerPlayerLevel.Easy)
                {
                    AddBrainCell(i_FirstSpot, firstSpotContent);
                    AddBrainCell(i_SecondSpot, secondSpotContent);
                }
            }

            return foundPair;
        }

        public bool Play(out SpotOnBoard o_FirstSpot, out SpotOnBoard o_SecondSpot, Board<T> io_Board)
        {
            if (m_ComputerLevel == eComputerPlayerLevel.Easy)
            {
                EasyMode(out o_FirstSpot, out o_SecondSpot, io_Board);
            }
            else if (m_ComputerLevel == eComputerPlayerLevel.Medium)
            {
                MediumMode(out o_FirstSpot, out o_SecondSpot, io_Board);
            }
            else
            {
                HardMode(out o_FirstSpot, out o_SecondSpot, io_Board);
            }

            return Turn(o_FirstSpot, o_SecondSpot, io_Board);
        }

        public void EasyMode(out SpotOnBoard o_FirstSpot, out SpotOnBoard o_SecondSpot, Board<T> io_Board)
        {
            o_FirstSpot = io_Board.GenerateRandomUnflippedSpotOnBoard();
            io_Board.FlipCard(o_FirstSpot.Row, o_FirstSpot.Col);
            o_SecondSpot = io_Board.GenerateRandomUnflippedSpotOnBoard();
            io_Board.FlipCard(o_SecondSpot.Row, o_SecondSpot.Col);
        }

        public void MediumMode(out SpotOnBoard o_FirstSpot, out SpotOnBoard o_SecondSpot, Board<T> io_Board)
        {
            o_FirstSpot = io_Board.GenerateRandomUnflippedSpotOnBoard();
            io_Board.FlipCard(o_FirstSpot.Row, o_FirstSpot.Col);
            o_SecondSpot = FindPair(o_FirstSpot, io_Board);
            io_Board.FlipCard(o_SecondSpot.Row, o_SecondSpot.Col);
        }

        public void HardMode(out SpotOnBoard o_FirstSpot, out SpotOnBoard o_SecondSpot, Board<T> io_Board)
        {
            o_FirstSpot = new SpotOnBoard();
            o_SecondSpot = new SpotOnBoard();
            bool foundPair = HasAPairInBrain(ref o_FirstSpot, ref o_SecondSpot);

            if (foundPair)
            {
                io_Board.FlipCard(o_FirstSpot.Row, o_FirstSpot.Col);
                io_Board.FlipCard(o_SecondSpot.Row, o_SecondSpot.Col);
            }
            else
            {
                MediumMode(out o_FirstSpot, out o_SecondSpot, io_Board);
            }
        }

        public bool HasAPairInBrain(ref SpotOnBoard o_FirstSpot, ref SpotOnBoard o_SecondSpot)
        {
            bool foundPair = false;

            foreach (BrainCell cell1 in m_Brain)
            {
                foreach (BrainCell cell2 in m_Brain)
                {
                    if (!ReferenceEquals(cell1, cell2) && cell1.CardContent.Equals(cell2.CardContent) && !cell1.SpotOnBoard.IsEqual(cell2.SpotOnBoard))
                    {
                        o_FirstSpot = cell1.SpotOnBoard;
                        o_SecondSpot = cell2.SpotOnBoard;
                        foundPair = true;
                        m_Brain.Remove(cell1);
                        m_Brain.Remove(cell2);
                        break;
                    }
                }
                if (foundPair)
                {
                    break;
                }
            }

            return foundPair;
        }
    }
}