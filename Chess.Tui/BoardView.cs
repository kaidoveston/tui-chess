using Chess.Core;
using Terminal.Gui.Drawing;
using Terminal.Gui.ViewBase;
using TuiAttribute = Terminal.Gui.Drawing.Attribute;

namespace Chess.Tui;

/// <summary>Renders a <see cref="Board"/> as an 8x8 grid, each square 3 columns wide.</summary>
public sealed class BoardView : View
{
  private const int SquareWidth = 3;

  private readonly Board _board;

  public BoardView(Board board)
  {
    _board = board;
    Width = Board.Size * SquareWidth;
    Height = Board.Size;
  }

  protected override bool OnDrawingContent(DrawContext? context)
  {
    base.OnDrawingContent(context);

    for (int rank = 0; rank < Board.Size; rank++)
    {
      // Screen row 0 is the top of the view, which should show Black's
      // back rank (rank index 7), so the rank is flipped into a row.
      int row = Board.Size - 1 - rank;

      for (int file = 0; file < Board.Size; file++)
      {
        bool isLightSquare = (file + rank) % 2 != 0;
        Piece? piece = _board.GetPiece(file, rank);

        ColorName16 foreground = piece is null
            ? ColorName16.Black
            : piece.Color == PieceColor.White ? ColorName16.White : ColorName16.Black;
        ColorName16 background = isLightSquare ? ColorName16.Gray : ColorName16.DarkGray;

        SetAttribute(new TuiAttribute(foreground, background));

        Move(file * SquareWidth, row);
        AddStr(piece is null ? "   " : $" {piece.UnicodeSymbol} ");
      }
    }

    return true;
  }
}
