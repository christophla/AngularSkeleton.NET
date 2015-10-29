@echo off

rem ----------------------------------------------------------
rem This file installs the storage configuration
rem ----------------------------------------------------------

set msbuild=C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe

if NOT "%1"=="" set root=%1
if NOT "%2"=="" set incoming=%2
if NOT "%3"=="" set outgoing=%3
if NOT "%4"=="" set archived=%4
if NOT "%5"=="" set expired=%5
if NOT "%6"=="" set errorLog=%6

rem ---------------------------------------------------------

call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86
CALL %msbuild% Build.proj /target:Compile;BuildWebOutput;Package /property:DatabaseServer=localhost

echo.

GOTO DONE

rem ---------------------------------------------------------

:ERROR
pause
echo.
echo =========================================================
echo Error was encountered
echo =========================================================
echo.

EXIT /B 1

:DONE

echo.
echo =========================================================
echo Completed successfully :-)
echo =========================================================
echo.

PAUSE