# MarketDashboard
CA plugins system for crypto markets

## Декомпозиция задачи:
- Подготовить окружение с использованием подхода Clean Architecture;
- Реализовать возможность использования *.dll плагинов (реализующих интерфейс от MarketPlugin.Base) без перезапуска хоста;
- Написать плагин для Blockchain;
- Написать плагин для Beshchange;
- Сделать фоновый сервис для запроса данных по активированным плагинам;

> Проект написан на ASP.NET Core 6.0 с использованием: MediatoR,\
> AutoMapper, HtmlAgilityPack, DotNetZip, Serilog, RestSharp,\
> Newtonsoft.Json, ncrontab, EFC.\
