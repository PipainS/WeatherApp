# Зависимости

- .NET 8.0  
- Angular CLI 16.2.15  
- PostgreSQL 16  
- Node.js 18.20.4  
- Package Manager: npm 10.7.0  

# Установка и запуск WeatherApp

## 1. Клонирование репозитория
```
git clone https://github.com/PipainS/WeatherApp.git
```
## 2. Настройка Backend
Установка зависимостей
```
Copy
dotnet restore
```
## Настройка базы данных
Отредактируйте файл appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=weather_db;Username=postgres;Password=yourpassword"
}
```
## Применение миграций
```
dotnet ef database update
```
## Запуск сервера
```
dotnet run
```
Сервер доступен по адресам:
🌐 HTTP: http://localhost:5143
🔒 HTTPS: https://localhost:7008

## 3. Настройка Frontend
Установка зависимостей
```
cd ui16/weather-archive-frontend
npm install
```
## Запуск приложения
```
ng serve
```
Приложение откроется по адресу: http://localhost:4200
