# OxiZip
![Language](https://img.shields.io/badge/language-C%23-0E15C0)
![Number of lines](https://img.shields.io/tokei/lines/github/karolstawowski/OxiZip)
![Version](https://img.shields.io/badge/version-1.1.0.0-0E15C0) <br>

## Description
OxiZip is a file archiver for Windows platform (x64 only). Program allows you to create and unpack ZIP archives.

<img src="preview.png">

## Installation

OxiZip is portable - you don't have to install it. To use program, download a newest version zip file from section below, which contains all necessary files. After unpacking an archive, you can use program by starting OxiZip.exe.

Yes, I made this archive using OxiZip. 

And yes, you can unpack it using OxiZip located inside.

## Releases
| Version | Link |
|---------|------|
| 1.1 | [Download](https://github.com/karolstawowski/OxiZip/raw/master/Releases/OxiZip_1.1.zip) |
| 1.0 | [Download](https://github.com/karolstawowski/OxiZip/raw/master/Releases/OxiZip_1.0.zip) |

## Usage

 ### Packing ZIP archive
 From top to bottom options:
 - Target location for newly created ZIP
 - Name of archive
 - Compression level (without compression, fast, the best)
 - List with files to pack
 - Add files
 - Remove highlighted file
 - Remove all files from the list
 - Pack
 
 If file already exists, program asks you if you want to overwrite or add files to existing archive.
 
 ### Unpacking ZIP archive
 From top to bottom options:
 - Archive source location
 - Target location for unpacked files
 - Unpack

## Tools and technologies
C#, Visual Studio, Windows Forms, System.IO.Compression.
