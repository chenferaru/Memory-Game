namespace MemoryGameEngine
{
    public class GameManager<T>
    {
        private Board<T> m_Board;
        private HumanPlayer<T>[] m_HumanPlayers;
        private ComputerPlayer<T>[] m_ComputerPlayers;
        private int m_HumanPlayersCounter = 0;
        private int m_ComputerPlayersCounter = 0;
        private int m_NumOfPlayers;
        private int m_CurrentTurn = 1;

        public GameManager(int i_NumOfPlayers)
        {
            m_NumOfPlayers = i_NumOfPlayers;
        }

        public Board<T> Board
        {
            get
            {
                return m_Board;
            }
            set
            {
                m_Board = value;
            }
        }

        public void CreateHumanPlayer(string i_Name)
        {
            if (m_HumanPlayers == null)
            {
                m_HumanPlayers = new HumanPlayer<T>[m_NumOfPlayers];
            }

            m_HumanPlayers[m_HumanPlayersCounter++] = new HumanPlayer<T>(i_Name);
        }

        public void CreateComputerPlayer(int i_Level)
        {
            if (m_ComputerPlayers == null)
            {
                m_ComputerPlayers = new ComputerPlayer<T>[m_NumOfPlayers];
            }

            m_ComputerPlayers[m_ComputerPlayersCounter++] = new ComputerPlayer<T>(i_Level);
        }

        public HumanPlayer<T>[] HumanPlayers
        {
            get
            {
                return m_HumanPlayers;
            }
            set
            {
                m_HumanPlayers = value;
            }
        }

        public ComputerPlayer<T>[] ComputerPlayers
        {
            get
            {
                return m_ComputerPlayers;
            }
            set
            {
                m_ComputerPlayers = value;
            }
        }

        public void CreateAndInitializeBoard(T[] i_ElementsForBoard, int i_NumOfRows, int i_NumOfCols)
        {
            m_Board = new Board<T>(i_NumOfRows, i_NumOfCols);
            m_Board.InitializeBoard(i_ElementsForBoard);
        }

        public void EndUnsuccsessfulTurn(SpotOnBoard i_FirstSpot, SpotOnBoard i_SecondSpot)
        {
            m_Board.FlipCard(i_FirstSpot.Row, i_FirstSpot.Col);
            m_Board.FlipCard(i_SecondSpot.Row, i_SecondSpot.Col);
        }

        public int HumanPlayerCounter
        {
            get
            {
                return m_HumanPlayersCounter;
            }
            set
            {
                m_HumanPlayersCounter = value;
            }
        }

        public int ComputerPlayerCounter
        {
            get
            {
                return m_ComputerPlayersCounter;
            }
            set
            {
                m_ComputerPlayersCounter = value;
            }
        }

        public int NumOfPlayers
        {
            get
            {
                return m_NumOfPlayers;
            }
            set
            {
                m_NumOfPlayers = value;
            }
        }

        public int CurrentTurn
        {
            get
            {
                return m_CurrentTurn;
            }
            set
            {
                m_CurrentTurn = value;
            }
        }

        public void TurnGenarator(bool i_FoundPair)
        {
            if (!i_FoundPair)
            {
                if (m_CurrentTurn == m_NumOfPlayers)
                {
                    m_CurrentTurn = 1;
                }
                else
                {
                    m_CurrentTurn++;
                }
            }
        }
    }
}