# Project_template


# Задание 1. Анализ и планирование


### 1. Описание функциональности монолитного приложения

**Управление отоплением:**

- Пользователи могут удалённо включать/выключать отопление в своих домах.
- Система поддерживает управление отоплением в доме (/api/v1/sensors/:id/value)

**Мониторинг температуры:**

- Пользователи могут просматривать текущую температуру в своих домах через веб-интерфейс.
- Система поддерживает мониторинг температуры по одному датчику (/api/v1/sensors/:id) или по локации (/api/v1/sensors/temperature/:location)

### 2. Анализ архитектуры монолитного приложения

Приложение состоит из:
- монолитного сервиса на GO
- субд postgres
- сервиса temperature-api

взаимодействие сервиса на GO с базой с помощью pgx.

взаимодействие сервиса на GO с temperature-api через http

### 3. Определение доменов и границы контекстов

- управление устройствами
- мониторинг телеметрии
- слой данных 
- маршрутизация (api gateway)
- (возможно, слой представления, фронт)

### **4. Проблемы монолитного решения**
- единый свпособ взаимодействия со всеми типами устройств
- жесткая связанность доменов (изменения в одном домене требуют перекомпиляции всего приложения)
- единая база для всех доменов (все типы датчиков в одной базе)
- сложность в тестировании (необходимо развертывать все приложение для тестирования изменений в одном домене)
- один язык на весь проект без возможности вариативности

### 5. Визуализация контекста системы — диаграмма С4

[диаграмма контекста в модели C4](https://www.planttext.com?text=RPBBIiD058RtXRv3xAg2RJQkN0d5dO8ehgMcWmQIj6GcNdQzA2ug5g7hYle2lHWQUts5Cs_aVwOjhHS2cSoSV_xpdNEoBrKnL97lMONTyq8vKE9MfLDRRk1uKKbOLqfLv9vjXyMR_AMhhgABI8hGOLsWyaxPjojUCU3xEQbAddjX5tPpntnqTdHe-qMfH6YldVCLADEfT4gvoXCMjIcXBWrq5Uy9dgv58vhHLDSfff6kOSNH9urrQn6Pq1pIAQubLeRUzGlsOzsaBrOXd67pOn7ASf1r8gdAlWXte7n5SAoVQ8YrJrtZZts6OnyqDm1zJaEnhbiqWs36rf1nifWc5Z8Q8Hl4khgK6YUcYjb3rtK3eKPccpbExr2-dvLcuKplqFxYvUCEzN2zPamUaGs02um9zu8IXe5xWky1O-Z6cdW5-2_p9iRvmQtAbiIrwmZ9ITk6bjEDk3AuLhadxDHVma7nO03ViEk1WojyNXKzDeM3az7qi-bFOgf0gp72bf4kfyOp-zJDVz1rQq5v8hoDYQrxzdSwW8uDxD3YKL0lnwrX9hJ-7WTVkiQUxBZosIYy0-MVWG-nDSF5FzSqfVpWNY3kYw24__SR)

# Задание 2. Проектирование микросервисной архитектуры

**Диаграмма контейнеров (Containers)**

[Диаграмма контейнеров](https://www.planttext.com?text=ZLPDRzj64BqRy7_OwcG1adN9gQVOLf2ujLlPL9kZiPBMCX7n0-Ighb04a6KxGQ4WIj2le8SMJL3qK1HWP2jMR4i2yWkMVw6_f3Ep9FL5jBNXHBlSdNdpvisZxlgIUx9fDTZMdOCjnvRSj8MtklAHQLSRpPfWHrAw_iU5WiUFr-kcF6fMchxmgd1Is7AzwbW5jy5jZ93tPQrXLWfRT_8btFfYTwTWSLyAhp0RUzs5iwihgolIb0t1adrMD7dTunRxk_qTKt-fQpKEJzGmF0axQg1kLS0-_AbUXo_LHDs4NVMUgKirLhSVRZ1MINY-OwyXj1pBg1_X7jww2PzbSFr6JSAs2bGVTdfg47QYo_YNO_GGaq6Ii0EVXt0me6Cw2kKVw4VXyqmMCnejgCmwkFUrNAk9hyogy2aHmiLewZq1XpWG7iAsOJ4ANzAZFc7wdf8zXMJNQfXZy8Go0PnhI8xvsc4NLds0Xl0wAe0Lmkp3is_Xz0Zk3jQP-XLYNi8lt53lDFZmEO97104Skqm0TDM0RJlbV5Ho1su2pqZeN5MRJjEkSQ-rvjCQu1gS6tcadb2V4QP_wKacoxx1U0n-aWwl7OiATrs8zb1Ks8RhDimgbwPZ8nz6QOEjxGjUbOKlco9B5Bq2n1qYuWpA1KQX9E9c3BzluVDDU1fNZ6o4FGG0BMQmdtHSCwMVHJrdQiXWCvXs2vk0X3sXqm69O02ToYvNmLqJm6sKjjbzBiKnRo7M_Nj6cHNOVO-xHtixXFu7o3UaO3tKqHXwVOrow8VdrB0WBNYj0bTXVEIXurcio2MlS5zWl9BZoxed38oUePw3kjKLwWbxGkf17PqkIp0NbvwQKlgKj2mQmXBIQp53EXwlKzAoQOay39KfV5RSfB9-9rv7iVvIHWIPN4AHbjaIlW-9DZtd4StjhbuplO7PFkM7ZpZmkiyh5LFkxX625-WB88rpYZ-AA2P98BIt96oa6ixXPXUMGnJda97QR_LGf24oxRhmvHmRsxG5AcM6y73GCz5ZCAj3mgFdRq9UrOsj0pxtI6DiTwzSPgGk07QksOjSACrUG22iR03P2Rna6hNH51qBm4BY8YsPNiVeV8Bq6vb1NqEAXuACvHr9uKAD6R4oGPfIcZKTtmMn2CkDCX4DbYiyBfkUWFosxtYp9Cq38ro_BFSeLJxQyYTK1HhEQMA60SgS7Eq93dLkhibOfBuqK1VqRtzMy_Eb70am7xk-BngyDItfWTvbjFqVrRnPD1aSXyZ7MMIlawa1vT8rYYPD9PxGfA92Hr4xOCBHBxeG1HRnqEbMpkaOAtki_ukBtU6jW_t54TFwpiwUY2MFDW0F7kDzA3vwboQMhLu1ladiEk2lfD-d4TbNJ1S6-rUuXVGyA9TB1eB3Q76Sn3ppL1VP5xnyukE5JupFFzCDmrjqcjn9_K6Q88uBS2s8vktPz73o6bwq5VLwQSfwc147CyQtS6VcbVxpb6aOfJHxLRUpqNIycDKbB3DceLy7ezGXWAyP2mXd-uI1VffoW1ffwzjeXp0Bvw13y9XB-hQHW9eRtZZE3B5e544SONejTL2M2blWAHg367rgZFyFWOGmxpsQ0FtD3JrEpyqP1Bb0kz06eOOn08gGFl9BBLx-xg2Ht1Ls3Rwy_WC0)

**Диаграмма компонентов (Components)**

Добавьте диаграмму для каждого из выделенных микросервисов.

**Диаграмма кода (Code)**

Добавьте одну диаграмму или несколько.

# Задание 3. Разработка ER-диаграммы

Добавьте сюда ER-диаграмму. Она должна отражать ключевые сущности системы, их атрибуты и тип связей между ними.

# Задание 4. Создание и документирование API

### 1. Тип API

Укажите, какой тип API вы будете использовать для взаимодействия микросервисов. Объясните своё решение.

### 2. Документация API

Здесь приложите ссылки на документацию API для микросервисов, которые вы спроектировали в первой части проектной работы. Для документирования используйте Swagger/OpenAPI или AsyncAPI.

# Задание 5. Работа с docker и docker-compose

Перейдите в apps.

Там находится приложение-монолит для работы с датчиками температуры. В README.md описано как запустить решение.

Вам нужно:

1) сделать простое приложение temperature-api на любом удобном для вас языке программирования, которое при запросе /temperature?location= будет отдавать рандомное значение температуры.

Locations - название комнаты, sensorId - идентификатор названия комнаты

```
	// If no location is provided, use a default based on sensor ID
	if location == "" {
		switch sensorID {
		case "1":
			location = "Living Room"
		case "2":
			location = "Bedroom"
		case "3":
			location = "Kitchen"
		default:
			location = "Unknown"
		}
	}

	// If no sensor ID is provided, generate one based on location
	if sensorID == "" {
		switch location {
		case "Living Room":
			sensorID = "1"
		case "Bedroom":
			sensorID = "2"
		case "Kitchen":
			sensorID = "3"
		default:
			sensorID = "0"
		}
	}
```

2) Приложение следует упаковать в Docker и добавить в docker-compose. Порт по умолчанию должен быть 8081

3) Кроме того для smart_home приложения требуется база данных - добавьте в docker-compose файл настройки для запуска postgres с указанием скрипта инициализации ./smart_home/init.sql

Для проверки можно использовать Postman коллекцию smarthome-api.postman_collection.json и вызвать:

- Create Sensor
- Get All Sensors

Должно при каждом вызове отображаться разное значение температуры

Ревьюер будет проверять точно так же.


