# Portable Document Format (PDF) Plugin for Paint.NET

[![](https://img.shields.io/github/release-pre/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET.svg?style=flat)](https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/releases)
[![](https://img.shields.io/github/downloads/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/total.svg?style=flat)](https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/releases)

This is a Paint.NET file type plugin for opening and saving Portable Document Format `(PDF)` files.<br/>
It also has ability to open Adobe® Illustrator® Artwork `(AI)` files saved with PDF compatibility option.

Compatible with Paint.NET 5.1 and later.

### Download links

The easiest and recommended way to install the plugin is using the installer. It supports Store and Classic versions of Paint.NET.<br/>
If you're using the portable version of Paint.NET please use the manual installation method.

| Installer (Classic/Store) | Manual Installation (Portable) |
|:-------------------------:|:------------------------------:|
| [PdfFileTypePlugin_setup.exe](https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/releases/latest/download/PdfFileTypePlugin_setup.exe) | [PdfFileTypePlugin.zip](https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET/releases/latest/download/PdfFileTypePlugin.zip) |

### How to install

**Note: Before installation please make sure you don't have any other file type plugin installed handling the same file types as this plugin.**

To auto install the plugin perform the following steps:
  * Download and run `PdfFileTypePlugin_setup.exe`
  * Follow the steps of the setup wizard.

To manually install the plugin perform the following steps:
  * Download and extract `PdfFileTypePlugin.zip`

  * If you're using Classic (All users) or Portable version of Paint.NET:
	* Put the `PdfFileTypePlugin` folder with its contents in the `<Paint.NET>\FileTypes` directory (default location is `C:\Program Files\paint.net\FileTypes`).
  * If you're using Classic (Just for yourself) or Store version:
    * Put the `PdfFileTypePlugin` folder with its contents in the `<Documents>\paint.net App Files\FileTypes` directory.

  * Optional steps:
	* If you want to disable Save PDF functionality, create an empty text file named `PdfFileType.DisableSave.txt` in the same directory.
	* If you want to disable Adobe® Illustrator® Artwork `(.ai)` support, create an empty text file named `PdfFileType.DisableAi.txt` in the same directory.

  * Restart Paint.NET.