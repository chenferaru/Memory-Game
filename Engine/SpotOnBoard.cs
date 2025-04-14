namespace MemoryGameEngine
{
    public struct SpotOnBoard
    {
        private int m_Row;
        private int m_Col;

        public int Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }
            set
            {
                m_Col = value;
            }
        }

        public SpotOnBoard(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public bool IsEqual(SpotOnBoard i_OtherSpotOnBoard)
        {
            return m_Row == i_OtherSpotOnBoard.m_Row && m_Col == i_OtherSpotOnBoard.m_Col;
        }
    }
}