namespace Chess.Core;

/// <summary>
/// Immutable piece data. Move legality is not the piece's concern —
/// it lives in a separate rules/move-generator type that has board context.
/// </summary>
public sealed class Piece
{
  public PieceType Type { get; }
  public PieceColor Color { get; }

  // Needed for castling (rook/king) and pawn double-step eligibility.
  public bool HasMoved { get; }

  public Piece(PieceType type, PieceColor color, bool hasMoved = false)
  {
    Type = type;
    Color = color;
    HasMoved = hasMoved;
  }

  // Boards are arrays of Piece references; moving a piece swaps in a new
  // instance rather than mutating one that other snapshots might hold.
  public Piece Moved() => new(Type, Color, hasMoved: true);

  public char Symbol => Type switch
  {
    PieceType.Pawn => 'P',
    PieceType.Knight => 'N',
    PieceType.Bishop => 'B',
    PieceType.Rook => 'R',
    PieceType.Queen => 'Q',
    PieceType.King => 'K',
    _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
  };

  // White pieces render uppercase, black lowercase — standard FEN/PGN convention.
  public override string ToString() =>
      (Color == PieceColor.White ? Symbol : char.ToLowerInvariant(Symbol)).ToString();
}
