namespace FToolkit.AnalyzerUtilities.Buffers;

/// <summary>
/// ソースコードのブロックを表します。
/// </summary>
public readonly ref struct SourceCodeWriterBlock : IDisposable
{
    readonly SourceCodeWriter _writer;

    /// <summary>
    /// <see cref="SourceCodeWriterBlock"/>構造体の新しいインスタンスを作成します。
    /// </summary>
    /// <param name="writer">ソースコードを書き込むクラスのインスタンス</param>
    /// <exception cref="ArgumentNullException"><paramref name="writer"/>が<see langword="null"/>です。</exception>
    public SourceCodeWriterBlock(SourceCodeWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        _writer = writer;
    }

    static ReadOnlySpan<char> CloseBlock => "}".AsSpan();

    /// <inheritdoc/>
    public void Dispose()
    {
        _writer.DecreaseIndent();
        _writer.WriteLine(CloseBlock);
        _writer.WriteLine();
    }
}
