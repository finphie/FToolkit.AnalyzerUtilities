using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class SourceCodeWriterWriteBlockTest
{
    [Test]
    public async Task ブロック1つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.WriteBlock())
        {
            writer.WriteLine("value2");
        }

        await Assert.That(writer.ToString())
            .IsEqualTo("""
                value1
                {
                    value2
                }

                """);
    }

    [Test]
    public async Task ブロック2つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.WriteBlock())
        {
            writer.WriteLine("value2");

            using (writer.WriteBlock())
            {
                writer.WriteLine("value3");
            }
        }

        await Assert.That(writer.ToString())
            .IsEqualTo("""
                value1
                {
                    value2
                    {
                        value3
                    }
                }

                """);
    }

    [Test]
    public async Task ブロック開始位置が行頭_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();

        await Assert.That(() => writer.WriteBlock())
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task ブロック開始位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("value");

        await Assert.That(() => writer.WriteBlock())
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task ブロック終了位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        var action = () =>
        {
            using (writer.WriteBlock())
            {
                writer.Write("value2");
            }
        };
        await Assert.That(action)
            .ThrowsExactly<InvalidOperationException>();
    }
}
