# FToolkit.AnalyzerUtilities

[![Azure Artifacts](https://feeds.dev.azure.com/finphie/7af9aa4d-c550-43af-87a5-01539b2d9934/_apis/public/Packaging/Feeds/DotNet/Packages/7a2f4db0-1043-4d11-92e1-af8b31788070/Badge)](https://dev.azure.com/finphie/Main/_artifacts/feed/DotNet/NuGet/FToolkit.AnalyzerUtilities?preferRelease=true)

アナライザー関連のユーティリティライブラリです。

## 説明

FToolkit.AnalyzerUtilitiesは、アナライザーやソースジェネレーターに必要な処理を詰め合わせたライブラリです。

> [!Important]
> 作者が必要な機能のみ実装しています。

> [!Warning]
> メジャーバージョンアップ以外でも破壊的変更を行うことがあります。

### Azure Artifacts（開発用ビルド）

```shell
dotnet add package FToolkit.AnalyzerUtilities -s https://pkgs.dev.azure.com/finphie/Main/_packaging/DotNet/nuget/v3/index.json
```

## 作者

finphie

## ライセンス

MIT

## クレジット

このプロジェクトでは、次のライブラリ等を使用しています。

### テスト

- [Microsoft.Testing.Extensions.CodeCoverage](https://github.com/microsoft/codecoverage)
- [Shouldly](https://github.com/shouldly/shouldly)
- [xunit.v3](https://github.com/xunit/xunit)

### アナライザー

- [DocumentationAnalyzers](https://github.com/DotNetAnalyzers/DocumentationAnalyzers)
- [IDisposableAnalyzers](https://github.com/DotNetAnalyzers/IDisposableAnalyzers)
- [Microsoft.CodeAnalysis.NetAnalyzers](https://github.com/dotnet/sdk)
- [Microsoft.VisualStudio.Threading.Analyzers](https://github.com/Microsoft/vs-threading)
- [Roslynator.Analyzers](https://github.com/dotnet/roslynator)
- [Roslynator.Formatting.Analyzers](https://github.com/dotnet/roslynator)
- [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
