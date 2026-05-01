using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterGetSpanTest
{
    [Test]
    public async Task 要求サイズが0_空でないSpanを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var span = writer.GetSpan(0);

        await Assert.That(span.IsEmpty)
            .IsFalse();
    }

    [Test]
    public async Task 要求サイズが1以上_1以上のバッファーを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var span = writer.GetSpan(100);

        await Assert.That(span.Length)
            .IsGreaterThanOrEqualTo(100);
    }

    [Test]
    public async Task バッファー拡張_元データが保持される()
    {
        using var writer = new ArrayPoolBufferWriter<int>();

        var span = writer.GetSpan(4);
        span[0] = 1;
        span[1] = 2;
        writer.Advance(2);

        writer.GetSpan(span.Length);

        await Assert.That(writer.WrittenSpan.Length)
            .IsEqualTo(2);
        await Assert.That(writer.WrittenSpan[0])
            .IsEqualTo(1);
        await Assert.That(writer.WrittenSpan[1])
            .IsEqualTo(2);
    }

    [Test]
    public async Task 要求サイズが負_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        await Assert.That(() => writer.GetSpan(-1))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
