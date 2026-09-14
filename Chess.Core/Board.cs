namespace Chess.Core;

/// <summary>
/// An 8x8 grid of squares, set up in the standard starting position.
/// File 0-7 = a-h; rank 0-7 = ranks 1-8, so White's back rank is rank 0.
/// </summary>
public sealed class Board
{
  public const int Size = 8;

  private static readonly PieceType[] BackRankOrder =
  [
      PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen,
        PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook
  ];

  private readonly Piece?[,] _squares = new Piece?[Size, Size];

  public Board()
  {
    SetBackRank(rank: 0, PieceColor.White);
    SetPawnRank(rank: 1, PieceColor.White);
    SetPawnRank(rank: 6, PieceColor.Black);
    SetBackRank(rank: 7, PieceColor.Black);
  }

  public Piece? GetPiece(int file, int rank) => _squares[file, rank];

  public void SetPiece(int file, int rank, Piece? piece) => _squares[file, rank] = piece;

  private void SetPawnRank(int rank, PieceColor color)
  {
    for (int file = 0; file < Size; file++)
    {
      _squares[file, rank] = new Piece(PieceType.Pawn, color);
    }
  }

  private void SetBackRank(int rank, PieceColor color)
  {
    for (int file = 0; file < Size; file++)
    {
      _squares[file, rank] = new Piece(BackRankOrder[file], color);
    }
  }
}
