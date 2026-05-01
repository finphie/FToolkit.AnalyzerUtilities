using FToolkit.AnalyzerUtilities.Buffers;

namespace FToolkit.AnalyzerUtilities.Tests.Buffers;

public sealed class SourceCodeWriterIndentTest
{
    [Test]
    public async Task インデント1つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.Indent())
        {
            writer.WriteLine("value2");
        }

        await Assert.That(writer.ToString())
            .IsEqualTo("""
                value1
                    value2

                """);
    }

    [Test]
    public async Task インデント2つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.Indent())
        {
            writer.WriteLine("value2");

            using (writer.Indent())
            {
                writer.WriteLine("value3");
            }
        }

        await Assert.That(writer.ToString())
            .IsEqualTo("""
                value1
                    value2
                        value3

                """);
    }

    [Test]
    public async Task インデント6つ()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        using (writer.Indent())
        {
            writer.WriteLine("value2");

            using (writer.Indent())
            {
                writer.WriteLine("value3");

                using (writer.Indent())
                {
                    writer.WriteLine("value4");

                    using (writer.Indent())
                    {
                        writer.WriteLine("value5");

                        using (writer.Indent())
                        {
                            writer.WriteLine("value6");

                            using (writer.Indent())
                            {
                                writer.WriteLine("value7");
                            }
                        }
                    }
                }
            }
        }

        await Assert.That(writer.ToString())
            .IsEqualTo("""
                value1
                    value2
                        value3
                            value4
                                value5
                                    value6
                                        value7

                """);
    }

    [Test]
    public async Task インデント開始位置が行頭_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();

        await Assert.That(() => writer.Indent())
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task インデント開始位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.Write("value");

        await Assert.That(() => writer.Indent())
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task インデント終了位置が改行以外_InvalidOperationException()
    {
        using var writer = new SourceCodeWriter();
        writer.WriteLine("value1");

        var action = () =>
        {
            using (writer.Indent())
            {
                writer.Write("value2");
            }
        };
        await Assert.That(action)
            .ThrowsExactly<InvalidOperationException>();
    }
}
