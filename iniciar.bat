@echo off
echo Iniciando backend...
start cmd /k "cd /d %~dp0backend\ControleGastos.API && dotnet run --launch-profile http"

timeout /t 3 /nobreak > nul

echo Iniciando frontend...
if not exist "%~dp0frontend\node_modules" (
    echo Instalando dependências do frontend pela primeira vez...
    start cmd /k "cd /d %~dp0frontend && npm install && npm run dev"
) else (
    start cmd /k "cd /d %~dp0frontend && npm run dev"
)

echo.
echo ✔ Backend:  http://localhost:5176
echo ✔ Frontend: http://localhost:5173
echo.
pause