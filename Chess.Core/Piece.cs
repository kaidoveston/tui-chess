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

  // Unicode has a distinct glyph per color (not a case variant like the ASCII
  // Symbol), so this switches on both Type and Color via a tuple pattern.
  public char UnicodeSymbol => (Color, Type) switch
  {
    (PieceColor.White, PieceType.Pawn) => '♙',
    (PieceColor.White, PieceType.Knight) => '♘',
    (PieceColor.White, PieceType.Bishop) => '♗',
    (PieceColor.White, PieceType.Rook) => '♖',
    (PieceColor.White, PieceType.Queen) => '♕',
    (PieceColor.White, PieceType.King) => '♔',
    (PieceColor.Black, PieceType.Pawn) => '♟',
    (PieceColor.Black, PieceType.Knight) => '♞',
    (PieceColor.Black, PieceType.Bishop) => '♝',
    (PieceColor.Black, PieceType.Rook) => '♜',
    (PieceColor.Black, PieceType.Queen) => '♛',
    (PieceColor.Black, PieceType.King) => '♚',
    _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
  };

  // White pieces render uppercase, black lowercase — standard FEN/PGN convention.
  // Kept as-is (rather than switched to Unicode) since plain ASCII letters are
  // what FEN/PGN export and other engines expect.
  public override string ToString() =>
      (Color == PieceColor.White ? Symbol : char.ToLowerInvariant(Symbol)).ToString();
}
