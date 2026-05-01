using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterAdvanceTest
{
    [Test]
    public async Task WrittenSpanが更新される()
    {
        using var writer = new ArrayPoolBufferWriter<int>();
        var span = writer.GetSpan(4);

        span[0] = 1;
        span[1] = 2;
        span[2] = 3;

        writer.Advance(3);

        await Assert.That(writer.WrittenSpan.Length)
            .IsEqualTo(3);
        await Assert.That(writer.WrittenSpan[0])
            .IsEqualTo(1);
        await Assert.That(writer.WrittenSpan[1])
            .IsEqualTo(2);
        await Assert.That(writer.WrittenSpan[2])
            .IsEqualTo(3);
    }

    [Test]
    public async Task 書き込まれたデータ数が負_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        await Assert.That(() => writer.Advance(-1))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task 書き込まれたデータ数がバッファーサイズを超える_ArgumentOutOfRangeException()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var length = writer.GetSpan().Length;

        await Assert.That(() => writer.Advance(length + 1))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
