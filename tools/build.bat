
@ECHO OFF

echo:
echo ===========================
echo GeocodeFarm package builder
echo ===========================
echo:

set currentDirectory=%CD%

cd ..
set rootDirectory=%CD%

if NOT EXIST build mkdir build
cd build
set outputDirectory=%CD%

cd %rootDirectory%
if NOT EXIST packages mkdir packages
cd packages
set packagesDirectory=%CD%

cd %currentDirectory%
set nuget=%CD%\..\tools\nuget.exe
set msbuild4="%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
set vincrement=%CD%\..\tools\Vincrement.exe


echo Check CLI apps
echo -----------------------------

if not exist %nuget% (
 echo ERROR: nuget could not be found, verify path. exiting.
 echo Configured as: %nuget%
 pause
 exit
)

if not exist %msbuild4% (
 echo ERROR: msbuild 4 could not be found, verify path. exiting.
 echo Configured as: %msbuild4%
 pause
 exit
)

if not exist %vincrement% (
 echo ERROR: vincrement could not be found, verify path. exiting.
 echo Configured as: %vincrement%
 pause
 exit
)

echo Everything is fine.

echo:
echo Build solution
echo -----------------------------

pause

cd ..
cd src
set solutionDirectory=%CD%
%msbuild4% "GeocodeFarm.sln" /p:Configuration=Release /nologo /verbosity:q

if not %ERRORLEVEL% == 0 (
 echo ERROR: build failed. exiting.
 cd %currentDirectory%
 pause
 exit
)
echo Done.

echo:
echo Copy libs
echo -----------------------------

if NOT EXIST %outputDirectory%\lib        mkdir %outputDirectory%\lib
if NOT EXIST %outputDirectory%\lib\net45  mkdir %outputDirectory%\lib\net45
if NOT EXIST %outputDirectory%\lib\net461 mkdir %outputDirectory%\lib\net461

xcopy /Q /Y %solutionDirectory%\GeocodeFarm\bin\net45\Release\GeocodeFarm.dll %outputDirectory%\lib\net45\
xcopy /Q /Y %solutionDirectory%\GeocodeFarm\bin\net45\Release\GeocodeFarm.xml %outputDirectory%\lib\net45\
xcopy /Q /Y %solutionDirectory%\GeocodeFarm\bin\net461\Release\GeocodeFarm.dll %outputDirectory%\lib\net461\
xcopy /Q /Y %solutionDirectory%\GeocodeFarm\bin\net461\Release\GeocodeFarm.xml %outputDirectory%\lib\net461\
echo Done.


echo:
echo Increment version number
echo -----------------------------

set /p version=<%rootDirectory%\version.txt
echo Previous version: %version%

echo Hit return to continue...
pause 
%vincrement% -file=%rootDirectory%\version.txt 0.0.1 %rootDirectory%\version.txt
if not %ERRORLEVEL% == 0 (
 echo ERROR: vincrement existed with code %ERRORLEVEL%. exiting.
 cd %currentDirectory%
 pause
 exit
)

set /p version=<%rootDirectory%\version.txt
echo New version:      %version%



echo:
echo Build NuGet package
echo -----------------------------

echo - Did you update the release notes?
echo:
echo Hit return to continue...
pause 
cd %outputDirectory%
%nuget% pack %rootDirectory%\GeocodeFarm.nuspec -BasePath %outputDirectory% -Version %version% -OutputDirectory %packagesDirectory%
echo Done.


echo:
echo Push NuGet package
echo -----------------------------

echo Hit return to continue...
pause 
cd %packagesDirectory%
%nuget% push %packagesDirectory%\GeocodeFarm.%version%.nupkg  -Source https://www.nuget.org/api/v2/package
echo Done.





cd %currentDirectory%
pause



