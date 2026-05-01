using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterGetMemoryTest
{
    [Test]
    public async Task 要求サイズが0_空でないMemoryを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var memory = writer.GetMemory(0);

        await Assert.That(memory)
            .IsNotEmpty();
    }

    [Test]
    public async Task 要求サイズが1以上_1以上のバッファーを返す()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        var memory = writer.GetMemory(100);

        await Assert.That(memory)
            .Count()
            .IsGreaterThanOrEqualTo(100);
    }

    [Test]
    public async Task バッファー拡張_元データが保持される()
    {
        using var writer = new ArrayPoolBufferWriter<int>();

        var memory = writer.GetMemory(4);
        memory.Span[0] = 1;
        memory.Span[1] = 2;
        writer.Advance(2);

        writer.GetMemory(memory.Length);

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

        await Assert.That(() => writer.GetMemory(-1))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
