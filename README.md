# Задание WPF – Анализатор курса валют
Необходимо реализовать WPF десктопное приложение, которое реализует следующую функциональность:
1. Собирает по заданному расписанию (CRON строка) курс валют в формате XML с сайта ЦБ РФ.
2. Синхронизирует справочник по кодам валют по заданному расписанию (CRON строка) с сайта ЦБ РФ.
3. Имеет гамбургер меню с набором страниц – Главная страница, Список курсов валют.
4. На главной странице должен отображаться крупным шрифтом актуальный курс доллара в рублях по умолчанию. Выпадающим списком можно изменить валюту, например, на евро.
5. На странице со списком курсов валют необходимо вывести всю собранную информацию постранично. Грид должен содержать следующие столбцы – «Дата» (когда был сбор информации по курсам валют), «Код валюты» (CharCode), «Наименование валюты», «Номинал», «Курс» (значение в рублях). Предусмотреть защиту от дублирования, то есть если было собрано несколько одинаковых курсов валют за день, то не выводить такие дубликаты.
6. Все собранные значения должны сохраняться в базе данных. Справочник по кодам валют так же должен быть сохранен в виде отдельной таблицы БД и использоваться для выпадающего списка на главной странице.
Расписание задаются в конфигурационном файле отдельно для каждого метода API.
7. Проект должен быть реализован на технологии WPF .net framework.
