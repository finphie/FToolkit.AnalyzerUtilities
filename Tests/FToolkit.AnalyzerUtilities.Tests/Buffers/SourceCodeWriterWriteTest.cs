using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class SourceCodeWriterWriteTest
{
    [Test]
    public async Task 文字列()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("class A");

        await Assert.That(writer.ToString())
            .IsEqualTo("class A");
    }

    [Test]
    public async Task 補間文字列()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"return {1};");

        await Assert.That(writer.ToString())
            .IsEqualTo("return 1;");
    }

    [Test]
    public async Task 開始位置が改行以外()
    {
        using var writer = new SourceCodeWriter();

        writer.Write("x:");
        writer.Write("class A");

        await Assert.That(writer.ToString())
            .IsEqualTo("x:class A");
    }

    [Test]
    public async Task IFormattable以外()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{(1, 2)}");

        await Assert.That(writer.ToString())
            .IsEqualTo("(1, 2)");
    }

    [Test]
    public async Task Null()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{(string?)null}");

        await Assert.That(writer.ToString())
            .IsEmpty();
    }

    [Test]
    public async Task 空文字()
    {
        using var writer = new SourceCodeWriter();
        writer.Write($"{string.Empty}");

        await Assert.That(writer.ToString())
            .IsEmpty();
    }
}
