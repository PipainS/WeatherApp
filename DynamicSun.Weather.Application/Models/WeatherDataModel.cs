﻿namespace DynamicSun.Weather.Application.Models
{
    public class WeatherDataModel
    {
        public WeatherDataModel() { }

        // Объединяем дату и время в одно поле для удобства
        public DateTime WeatherDateTime { get; set; }

        // Температура воздуха (°C)
        public double Temperature { get; set; }

        // Относительная влажность воздуха (%)
        public int RelativeHumidity { get; set; }

        // Точка росы (°C)
        public double DewPoint { get; set; }

        // Атмосферное давление (мм рт.ст.)
        public int AtmosphericPressure { get; set; }

        // Направление ветра (например, "З,ЮЗ" или "штиль")
        public string WindDirection { get; set; }

        // Скорость ветра (м/с)
        public double WindSpeed { get; set; }

        // Облачность (%)
        public int Cloudiness { get; set; }

        // Высота облачности (м) – в метрах
        public int CloudBaseHeight { get; set; }

        // Видимость (VV) – в метрах; может отсутствовать, поэтому делаем поле nullable
        public int? Visibility { get; set; }

        // Погодные явления (например, "Дымка")
        public string WeatherPhenomena { get; set; }
    }
}
