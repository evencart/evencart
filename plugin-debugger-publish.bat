set source=publish
set dest=debugger\EvenCartPluginDebugger
copy %source%\EvenCart.dll %dest%\EvenCart.dll
copy %source%\EvenCart.Infrastructure.dll %dest%\EvenCart.Infrastructure.dll
copy %source%\EvenCart.Services.dll %dest%\EvenCart.Services.dll
copy %source%\EvenCart.Data.dll %dest%\EvenCart.Data.dll
copy %source%\EvenCart.Core.dll %dest%\EvenCart.Core.dll
copy %source%\web.config %dest%\web.config
copy %source%\*.json %dest%
echo d | xcopy /S /E /Y %source%\App_Data\*.* %dest%\App_Data
echo d | xcopy /S /E /Y %source%\Areas\*.* %dest%\Areas
echo d | xcopy /S /E /Y %source%\Content\*.* %dest%\Content
echo d | xcopy /S /E /Y %source%\NativeLibs\*.* %dest%\NativeLibs
echo d | xcopy /S /E /Y %source%\Views\*.* %dest%\Views
echo d | xcopy /S /E /Y %source%\wwwroot\*.* %dest%\wwwroot
echo d | xcopy /S /E /Y %source%\Plugins\*.* %dest%\Plugins
echo d | xcopy /S /E /Y src\Build\*.* debugger\Build