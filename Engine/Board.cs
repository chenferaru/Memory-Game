using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGameEngine
{
    public class Board<T>
    {
        private readonly int r_NumOfColsInBoard;
        private readonly int r_NumOfRowsInBoard;
        private Card[,] m_Board;
        private int m_NumOfPairsLeftInBoard;

        private struct Card
        {
            private readonly T r_CardContent;
            private bool m_CardFlippedByPlayer;

            public Card(T i_CardContent)
            {
                r_CardContent = i_CardContent;
                m_CardFlippedByPlayer = false;
            }

            public T CardContent
            {
                get
                {
                    return r_CardContent;
                }
            }

            public bool CardFlippedByPlayer
            {
                get
                {
                    return m_CardFlippedByPlayer;
                }
                set
                {
                    m_CardFlippedByPlayer = value;
                }
            }
        }

        public Board(int i_NumOfRowsInBoard, int i_NumOfColsInBoard)
        {
            r_NumOfRowsInBoard = i_NumOfRowsInBoard;
            r_NumOfColsInBoard = i_NumOfColsInBoard;
            m_Board = new Card[i_NumOfRowsInBoard, i_NumOfColsInBoard];
            m_NumOfPairsLeftInBoard = (i_NumOfRowsInBoard * i_NumOfColsInBoard) / 2;
        }

        public void InitializeBoard(T[] i_ElementsForBoard)
        {
            int totalSlotsInBoard = r_NumOfRowsInBoard * r_NumOfColsInBoard;
            int numOfPairsInBoard = totalSlotsInBoard / 2;
            T[] cardsDeck = new T[totalSlotsInBoard];

            for (int i = 0; i < numOfPairsInBoard; i++)
            {
                cardsDeck[2 * i] = i_ElementsForBoard[i];
                cardsDeck[2 * i + 1] = i_ElementsForBoard[i];
            }

            shuffleDeckOfCards(cardsDeck);
            for (int rowIndex = 0, cardsDeckIndex = 0; rowIndex < r_NumOfRowsInBoard; rowIndex++)
            {
                for (int colIndex = 0; colIndex < r_NumOfColsInBoard; colIndex++)
                {
                    m_Board[rowIndex, colIndex] = new Card(cardsDeck[cardsDeckIndex]);
                    cardsDeckIndex++;
                }
            }
        }

        private void shuffleDeckOfCards(T[] io_Array)
        {
            Random random = new Random();
            int arrayLength = io_Array.Length;
            T temporaryCardContentHolder;
            int randomIndex;

            while (arrayLength > 1)
            {
                randomIndex = random.Next(arrayLength--);
                temporaryCardContentHolder = io_Array[arrayLength];
                io_Array[arrayLength] = io_Array[randomIndex];
                io_Array[randomIndex] = temporaryCardContentHolder;
            }
        }

        public int NumOfColsInBoard
        {
            get
            {
                return r_NumOfColsInBoard;
            }
        }

        public int NumOfRowsInBoard
        {
            get
            {
                return r_NumOfRowsInBoard;
            }
        }

        public int NumOfPairsLeftInBoard
        {
            get
            {
                return m_NumOfPairsLeftInBoard;
            }
            set
            {
                m_NumOfPairsLeftInBoard = value;
            }
        }

        public bool IsSpotTaken(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].CardFlippedByPlayer;
        }

        public T CardContent(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].CardContent;
        }

        public void FlipCard(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].CardFlippedByPlayer = !m_Board[i_Row, i_Col].CardFlippedByPlayer;
        }

        public SpotOnBoard GenerateRandomUnflippedSpotOnBoard()
        {
            List<SpotOnBoard> unflippedSpotsOnBoard = new List<SpotOnBoard>();
            Random randomIndexGenerator = new Random();

            for (int rowIndex = 0; rowIndex < NumOfRowsInBoard; rowIndex++)
            {
                for (int colIndex = 0; colIndex < NumOfColsInBoard; colIndex++)
                {
                    if (!(m_Board[rowIndex, colIndex].CardFlippedByPlayer))
                    {
                        unflippedSpotsOnBoard.Add(new SpotOnBoard(rowIndex, colIndex));
                    }
                }
            }

            int randomIndex = randomIndexGenerator.Next(0, unflippedSpotsOnBoard.Count);

            return unflippedSpotsOnBoard.ElementAt(randomIndex);
        }
    }
}