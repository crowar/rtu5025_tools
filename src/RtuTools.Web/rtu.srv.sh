#!/bin/bash

# Рабочая директория для приложения
APP_DIR=/opt/rtu.web

# Путь к исполняемому файлу приложения
APP_PATH=$APP_DIR/RtuTools.Web.dll

start() {
    # Проверка, запущено ли приложение
    PID=$(pgrep -f "dotnet $APP_PATH")
    if [ -n "$PID" ]; then
        echo "Приложение уже запущено (PID: $PID)."
    else
        # Запуск приложения
        cd $APP_DIR
        ASPNETCORE_URLS=http://0.0.0.0:5000 dotnet $APP_PATH &>/dev/null &
        echo "Приложение запущено."
    fi
}

stop() {
    # Остановка приложения
    PID=$(pgrep -f "dotnet $APP_PATH")
    if [ -n "$PID" ]; then
        kill $PID
        echo "Приложение остановлено."
    else
        echo "Приложение не запущено."
    fi
}

status() {
    # Проверка статуса приложения
    PID=$(pgrep -f "dotnet $APP_PATH")
    if [ -n "$PID" ]; then
        echo "Приложение запущено (PID: $PID)."
    else
        echo "Приложение не запущено."
    fi
}

case "$1" in
    start)
        start
        ;;
    stop)
        stop
        ;;
    restart)
        stop
        start
        ;;
    status)
        status
        ;;
    *)
        echo "Использование: $0 {start|stop|restart|status}"
        exit 1
        ;;
esac

exit 0
