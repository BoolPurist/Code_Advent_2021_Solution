using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.Bingo
{
  public class BingoGame
  {
    private int[] _randomNumbers;
    private List<BingoBoard> _boards = new();
    public BingoBoard WinningBoard { get; private set; }
    public int FinalResult => WinningBoard.SumOfUnMarkedFiels * WinningBoard.LastInsertedNumber;
    public int WinningNumber => WinningBoard.LastInsertedNumber;

    public BingoGame(string[] input)
    {
      ParseRandomNumbres(input[0]);
      ParseBoards();
     
      void ParseBoards()
      {
        bool parsingBoard = false;
        int boardLineIndex = 0;
        int[] boardBuffer = new int[25];
        for (int indexLine = 1; indexLine < input.Length; indexLine++)
        {
          string currentLine = input[indexLine];

          if (parsingBoard)
          {
            int[] oneLineOfBoard = SequenceUtility.CastToOneWholeNumberPerLine(currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            for (
              int indexOfBoard = boardLineIndex * 5, indexOfLine = 0, length = indexOfBoard + 5;
              indexOfBoard < length;
              indexOfBoard++, indexOfLine++
              )
            {
              boardBuffer[indexOfBoard] = oneLineOfBoard[indexOfLine];
            }

            boardLineIndex++;

            if (boardLineIndex == 5)
            {
              _boards.Add(new BingoBoard(boardBuffer));
              parsingBoard = false;
            }
          }
          else if (string.IsNullOrWhiteSpace(currentLine))
          {
            parsingBoard = true;
            boardLineIndex = 0;
            continue;
          }
        }
      }
    }

    public BingoBoard PlayTheGameAndGetLastWinningBoard()
    {
      List<BingoBoard> boardsNotWonYet = _boards;
      List<BingoBoard> winnigBoardThisRound = new();

      for (
        int i = 0; 
        i < _randomNumbers.Length && boardsNotWonYet.Count > 0;
        i++
        )
      {
        int currentRandomNumber = _randomNumbers[i];
        InsertNumberInAllBoard(currentRandomNumber, boardsNotWonYet);
        winnigBoardThisRound = GetWinningBoards(boardsNotWonYet);
        boardsNotWonYet.RemoveAll(element => winnigBoardThisRound.Contains(element));
      }


      BingoBoard lastWinningBoard = winnigBoardThisRound[^1];
      WinningBoard = lastWinningBoard;      
      return lastWinningBoard;
    }

    public BingoBoard PlayTheGameAndGetWinnerBoard()
    {
      foreach (int currentNumberToInsert in _randomNumbers)
      {
        InsertNumberInAllBoard(currentNumberToInsert, _boards);
        int winnerIndex = CheckForWinnerBoard();
        if (winnerIndex != -1)
        {
          WinningBoard = _boards[winnerIndex];
          return WinningBoard;
        }
      }

      return null;
    }

    private void InsertNumberInAllBoard(in int number, List<BingoBoard> boards)
    {
      foreach (BingoBoard board in boards)
      {
        board.TryInsertNumber(number);
      }
    }

    private List<BingoBoard> GetWinningBoards(List<BingoBoard> boards)
    {
      List<BingoBoard> winningBoards = new();
      foreach (BingoBoard board in boards)
      {
        if (board.HasWon)
        {
          winningBoards.Add(board);
        }
      }

      return winningBoards;
    }

    private int CheckForWinnerBoard()
    {
      for (int boardIndex = 0; boardIndex < _boards.Count; boardIndex++)
      {
        BingoBoard currentBingoBoard = _boards[boardIndex];
        if (currentBingoBoard.HasWon)
        {
          return boardIndex;
        }
      }

      return -1;
    }

    private void ParseRandomNumbres(string lineOfNumbers)
    {
      var splitted = lineOfNumbers.Split(',');
      _randomNumbers = SequenceUtility.CastToOneWholeNumberPerLine(splitted);
    }

    

    public override string ToString() => string.Join(' ', _randomNumbers);

    public string this[int boardIndex]
    {
      get => _boards[boardIndex].ToString();
    }

    public string GetStatusOfBoard(int boardIndex) => _boards[boardIndex].GetBoardStatus();

    public int BoardCount => _boards.Count;
  }
}
