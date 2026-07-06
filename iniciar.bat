@echo off
echo Iniciando backend...
start cmd /k "cd /d %~dp0backend\ControleGastos.API && dotnet run --launch-profile http"

timeout /t 3 /nobreak > nul

echo Iniciando frontend...
start cmd /k "cd /d %~dp0frontend && npm run dev"

echo.
echo ✔ Backend:  http://localhost:5176
echo ✔ Frontend: http://localhost:5173
echo.
pause