using Chess.Core;

namespace Chess.Tui;

/// <summary>
/// Block-character piece art, ported from the "CLASSIC" / "large" skin in
/// thomas-mauran/chess-tui's default_skins.json. Cells are 7 columns x 5 rows;
/// pieces narrower than 7 (pawn, knight) are centered with padding.
/// Both colors share the same shape — <see cref="BoardSpriteView"/> tints it
/// via the draw attribute's foreground, the same approach
/// <see cref="Piece.Symbol"/>/<see cref="Piece.UnicodeSymbol"/> use.
/// </summary>
internal static class PieceSprites
{
    public const int Width = 7;
    public const int Height = 5;

    public static readonly string[] Blank =
    [
        "       ",
        "       ",
        "       ",
        "       ",
        "       ",
    ];

    private static readonly string[] Pawn =
    [
        "       ",
        "       ",
        "  ▟█▙  ",
        "  ▜█▛  ",
        " ▟███▙ ",
    ];

    private static readonly string[] Rook =
    [
        "       ",
        " █▟█▙█ ",
        " ▜███▛ ",
        " ▐███▌ ",
        "▗█████▖",
    ];

    private static readonly string[] Knight =
    [
        "       ",
        " ▟▛██▙ ",
        "▟█████ ",
        "▀▀▟██▌ ",
        " ▟████ ",
    ];

    private static readonly string[] Bishop =
    [
        "       ",
        "   ⭘   ",
        "  █x█  ",
        "  ███  ",
        "▗█████▖",
    ];

    private static readonly string[] Queen =
    [
        "       ",
        "◀█▟█▙█▶",
        " ◥█◈█◤ ",
        "  ███  ",
        "▗█████▖",
    ];

    private static readonly string[] King =
    [
        "   ✚   ",
        " ▞▀▄▀▚ ",
        " ▙▄█▄▟ ",
        " ▐███▌ ",
        "▗█████▖",
    ];

    public static string[] Get(PieceType type) => type switch
    {
        PieceType.Pawn => Pawn,
        PieceType.Knight => Knight,
        PieceType.Bishop => Bishop,
        PieceType.Rook => Rook,
        PieceType.Queen => Queen,
        PieceType.King => King,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}
