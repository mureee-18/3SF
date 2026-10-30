# 3SF

.NET MAUI (.NET 10) のデスクトップアプリです。Windows を主なターゲットにしていますが、
MAUI なので Android / iOS / Mac Catalyst でもビルドできます。

## 必要環境

- Windows 10 / 11（Windows 向けビルド・実行）
- .NET 10 SDK と MAUI ワークロード（`dotnet workload list` で確認）
- Visual Studio 2022 / 2026（任意。`3SF.slnx` を開いて F5 でも起動できます）

## 起動方法

```powershell
dotnet build ThreeSF.csproj -f net10.0-windows10.0.19041.0
bin\Debug\net10.0-windows10.0.19041.0\win-x64\ThreeSF.exe
```

または `3SF.slnx` を Visual Studio で開いて F5。

## 構成

プロジェクトはソリューションディレクトリ直下にフラットに置いています（`src/` などの入れ子なし）。

```
3SF.slnx
ThreeSF.csproj                 … MAUI プロジェクト定義
App.xaml / App.xaml.cs         … リソース読み込み、起動時の初期化、ウィンドウサイズ
AppShell.xaml / .xaml.cs       … 左ナビゲーション（Shell）、フッターのテーマ切替とバージョン表示
MauiProgram.cs                 … フォント登録・ログ設定
Models/
  AppSettings.cs               … 保存する設定
  AppMetadata.cs               … バージョン・データフォルダー・設定ファイルパス
Services/
  AppServices.cs               … サービスと ViewModel の組み立て（簡易 DI）
  SettingsService.cs           … JSON 読み書き（壊れたファイルは既定値にフォールバック）
ViewModels/
  ViewModelBase.cs             … INotifyPropertyChanged の共通実装
  RelayCommand.cs              … ICommand の実装
  HomeViewModel.cs             … 挨拶・サンプル項目・稼働時間タイマー
  SettingsViewModel.cs         … 設定の編集・保存・テーマ即時適用
  AboutViewModel.cs            … 環境情報
Views/
  HomePage.xaml                … ホーム画面
  SettingsPage.xaml            … 設定画面
  AboutPage.xaml               … このアプリについて
Resources/Styles/
  Colors.xaml                  … ライト / ダークの配色
  Styles.xaml                  … 共通スタイル（カード・ボタン・入力・Shell）
Platforms/                     … 各プラットフォームのエントリーポイントとマニフェスト
```

## 画面を追加する手順

1. `Views/` に `ContentPage` を追加する（例: `Views/ReportPage.xaml`）
2. `ViewModels/` に `ViewModelBase` 継承クラスを追加し、`AppServices` で生成する
3. `AppShell.xaml` の `ShellContent` に登録する

```xml
<ShellContent Title="レポート"
              Route="report"
              ContentTemplate="{DataTemplate views:ReportPage}" />
```

## 設定の保存先

`%LOCALAPPDATA%\3SF\settings.json`
（Windows 以外では MAUI のアプリ専用データフォルダー配下）

保存される内容: 表示名、テーマ（ライト / ダーク）、最小化起動フラグ、最終保存日時。
「設定」画面の「設定フォルダーを開く」からも開けます。

## 今後の拡張ポイント

- 起動時の最小化（`AppSettings.StartMinimized` は保存済みだが未反映）
- ウィンドウ位置・サイズの保存
- `MauiProgram` の DI コンテナへ移行（現在は `AppServices` で手動組み立て）
- 単体テストプロジェクトの追加
