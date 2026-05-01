using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class ArrayPoolBufferWriterWrittenSpanTest
{
    [Test]
    public async Task 初期状態が空()
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        await Assert.That(writer.WrittenSpan.Length)
            .IsZero();
    }

    [Test]
    public async Task 複数回書き込み_WrittenSpanが正しい()
    {
        using var writer = new ArrayPoolBufferWriter<int>();

        var span1 = writer.GetSpan(2);
        span1[0] = 1;
        span1[1] = 2;
        writer.Advance(2);

        var span2 = writer.GetSpan(2);
        span2[0] = 3;
        span2[1] = 4;
        writer.Advance(2);

        await Assert.That(writer.WrittenSpan.Length)
            .IsEqualTo(4);
        await Assert.That(writer.WrittenSpan[0])
            .IsEqualTo(1);
        await Assert.That(writer.WrittenSpan[1])
            .IsEqualTo(2);
        await Assert.That(writer.WrittenSpan[2])
            .IsEqualTo(3);
        await Assert.That(writer.WrittenSpan[3])
            .IsEqualTo(4);
    }
}
