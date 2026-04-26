namespace FToolkit.AnalyzerUtilities;

/// <summary>
/// 定数を定義するクラスです。
/// </summary>
static class Constants
{
    /// <summary>
    /// 改行コード
    /// </summary>
    public static ReadOnlySpan<char> DefaultNewLine => "\r\n";

    /// <summary>
    /// ブロックの開始記号
    /// </summary>
    public static ReadOnlySpan<char> OpenBlock => "{";

    /// <summary>
    /// ブロックの終了記号
    /// </summary>
    public static ReadOnlySpan<char> CloseBlock => "}";
}
