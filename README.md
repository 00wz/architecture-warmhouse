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

[API Gateway](https://www.planttext.com?text=dLJTQXD15BuFv1sEkKh1PczwvLLh8bPe8URdkaoICHtOtLbc9vOWGchHcmY5AWXUUECBXD9WR9lc5Sw-Wa_YcSt-vaVKa6LdlZddEz-SdzttDLDwuDXGtJkfIiUJBdTriV18k5rxqEDmghNdFx4inSugVQ5F1vs1prLNkfhiAbtfM9xDNCEmw-kUBJfMTM-tRg3syP7bC5zpPMMvAnxP5Wl5WXRQvf3WK1EihvW3lqQVuA3-0fuppS_Oq9XMAHmJBbSdJ-N0xJ4rB3DFH0QFePGnB-t06-C2z4kuopt-MdIveL7IjhaYdre8GOgL26myQxPM8FoEYs244xp6EvpY76SuXU0YE0_U4Vo0FysA3YTuZpCm_rj2xsa_nvibMjfPLsIocDFJ9k0_rU228ju57_47lKa9wPdI_Z9HT1Cfsg9bcPqcL-QLfkR0yspHPLf85w938s4BZDz8rorEeyHCWWyu2ou1huDnf8kUM8fHFW_6mNi8pibbHASpMf759dsQErxL5hIaE2tQSCNqGF4uF2mFZOh3LgkUsU9L73qOGzxJzQLAz6y8ghZDXadKGquNTllGCEW_njtekwqd6joJlj1I3UEUI173NEVA5pHKRfJrp_WGPlm25zHvG9EdNqdbG8rfrc5-5ETjReh2XARb_aDUJ3D8XvpWM7ILz5FjALcOg9Hjyst_WgZMAVSwvePhLp2Wz7LVyURBe-M5Y7t3KAysCdvDHv4AOG7LP18Ea4dQs50PejLPNAj3jT6k0L4jmbuDMpkUf5H84ZRNhlWbZGZBmQI79Y2SaeXc0qc-_R8NmGcGPyYJIiZ6p5VNT6OS8qUwMhV8Bar9DWLXPPBixtEtHr_ntm00)

[Сервис мониторинга температуры(Temperature Sensor Service)](https://www.planttext.com?text=ZLJ9QXj14BqBz0yLdMo8f8jFELbB20O7PCavczPCIMwOBTqzCI8476Sxv1120ZdwcAkm8-BOblKBDR-GBqbrInejbY6wQ6fxzUfLpUng8vH9mm0QEqUDE4pY22DJB3oIaHUaFiAnCOb-KgqgSLBfItESTbEDoeiZmtaLBmwhII0YYr3MnWzajzhOARUiwyNp_Meej45LNSQk99nRB1GBHfe08VT3Kug-4Y7yFVq6XnWcg8H95K878nqh_bElf8UmnRsrrBRzjeLfC1KX8rH7zJYDVA46MuQhP-cFeVGmLcaRNbi8u5_EO-jbYcfWKLKS1AWSGe0X6ZM00nk3HR34qVRJpk6QYywpKxhAlj8jq6yQqeJDk-mjtT45yCCPr5fxy4mOF163qlO359OdMEq_fy-XMf84qXD6nj6ocywvrqtsBlj48nhJ5Os0he2PN3AJ4LtICFl81Bz0TiRcBKren85XTiObfpJEFcSVW7vn_dXgR29fjD-j-I8nKuqwpJhCJCjWB-efNhvAFITyNkROVRTgs6Oq0Rg6nNmT4olHnqtjUEqzsMzZ4cl9MGF6QQAxY8QBm29aUxHGJQza9iXFD-bCYcjMPvozP-FI3UpKO87OiBHuMVJ7Fj453IsNDok7rkowsLSMCoTkdJB4ChyZ4ZKqwxPykTBLyVjXUh4AeIcCw0hjwbgnDds5dOFzwVXj3ExVujfbq8_5TTrRM0wGvw-kRCEHE95kt4cCd12ioXngfNWcWBl7DRfvjmqR-wyrp1lUhtSzSu5sCVBvM_8F)

[Сервис управления отоплением(Heating Relay Service)](https://www.planttext.com?text=XLJBJXj14BmZyGyTdumKs0kdd00xIb2mv5VEQEnjp8fzPNOso8eYWPqWHHmYb0_W4YbN4s5X29XVcFc5V4bwPX_sOgFO1rizsrtLLJMx5Gd6HUovKDdShmHU6FZeYzML5uxVTMCRuL28C7fbMPmTR_GSSHXtuWXvD_05FRVH3JmhT9cl9wn7mdQTZbNPNA_hqllQdkMnI22tvcTlXFJiwihgYd24Yv3NeUgm7cSU_3tv0M-H2SVlGHDTreSMyezE5w5Ci94JQUGrFQ52B9ZZ8z_V2MBVPhnVvheXVVubb9REAQt19zqEzCdXQRRdCT_MCtdWkiYflPBKO5OiKRNvkjL-KfANwaHEr8My1tadf_9UZkI3l0Orb8zqCfA_vHyvfj94VGSvLGFOhk_26oRmcFLBQy-ocMsIR90HoZQXW-qmT9qkhHduysN3Q4guO_gzaoF8Y82SWFf274w8vIHzIBETo761l3fVHipx8CIspK9X1AfX5B4UmWuFZf13Ug3nToaWPAUXbNUdn8RoYkIOQ5hgB96aW0l4Qgm5MqP0e8iU2jvl8hCDYNPMWQIKm1zmoXwFkoBc-8J1BpL8pD0RFv8KfyQTQ-FFLrFItEHDgiijQIPleDsgxYnZH5T8O1F38794mFkvMJKKp6Q2mUnCmzUH9o5EZIgmkIU_1WHxRQ0Vr3dnKKDrgWR6gLlzJ_kbUNmkt86obtYHMB7WZOPwnmwE65ZGP9sE8seDFIJlhtRA8h8x1JrRn9b6wDwsu-4wtNm78qXacEyqVNJhnK70FQZIqXqMcRvw48aUnrPZBv6Ef5bsrHRoBYzDShKh8tL15wOGaQ4UDZSgxol6K_wSvPnIHz82UJsbxvNwbecUZIgqFXLpMMhrUhL6kxsmMCvcCQi5jyrVdJpZkdw3o4d6Pg4rDQSH8-y_ZxYOnV_cAqDSQ3LukN5RwDlqvlu7)

Компоненты Telemetry Storage (InfluxDB), Message Broker (Mosquitto MQTT) и Platform Database (PostgreSQL) являются готовыми промышленными продуктами.

**Диаграмма кода (Code)**

[API Gateway](https://www.planttext.com?text=XP9HQiCm38RV0xc37pj6Bh27qfAmhMyZxGNKM7G6nJQoiXB6O7VO3NUI9MdJfKr2_MP9_oVvbrT1WAKgAOxYI8mGgjHfL9c10qEfVhz_rFfjerv0y0Yrol335AYUoHrRHK4GmZcOEYliY93LPnofvZmUK4wv3CBxtW7hXUxk6_tmf5v5V8x1EnimZhvkUMlloHGWnjajSjUsv-uhGtgIUTFk2hl3qYE3L8ndO4e6hGp7tI5XYSBr43eGvkWTonKsHu9w5hYLnhaXx5JUHYVUV1cyHoJottjoaAw8Bym9_cWxIRASjRWldp2_bSrPU4SsjkYEOBJ6E5gXrSq__GC0)

# Задание 3. Разработка ER-диаграммы

[ER-диаграмма](https://www.planttext.com?text=dLJDIiGm4BuN-WwvUP1z0KyULj235tNFPMp6RI1fIvAk51NKgoVn3RoAu471FnHyWlGLV18driNTT9V2vjBsctI-Rxx9PDDOqBPICewii1BPrXuR2XXhKEpx_8QvU_VgFfeBzznSD9VkoRsx1_PrvswRQ_VftfehzyBSe_jmxrzlSHH7c5EQceqCQdOIHupMkk1iWur6EyFsUp1edwX0I8eOgqK-Rh4IZ3akD4yoC5aGEwgaJ79G6E2wa26KQWIBF05B0IiKKfMgZAEp6OdRHMMmJ-C2Ek1Sep4hC7fJvbdfNIS1zNhcjuJeL12aAP4d4YSuwzqipH0d8ksjR26BhSiEHX85aF6LEgIUpbUzm1wgsrQXEo8dMCeUVrhX1wHXXRPmDAaMfHL5liINtO9NiZSr_nKHch20y4XeTGmQambgivpr02KgjBeUWeNLoFtl8arySm8D4v2LntXH7Khy6mTCQO1whDv32VK-jGgtXR65hbTJvflTcRqCyl4o-ZLAs5qUzufyyqmcIhDyOnpzNYsdfuD1SV8_KN7K3bON3oBj6OkZcRCMHAQnATuUcn2TQsiOxBZhisvYpkcU_G40)

# Задание 4. Создание и документирование API

### 1. Тип API

REST как основной, MQTT для взаимодействия с Message Broker для асинхронного взаимодействия т.к. классика для iot.

### 2. Документация API

[Gateway](https://app.swaggerhub.com/apis/mayor-24e/warmhouse0/1.0.0)

[Temperature Sensor Service](https://app.swaggerhub.com/apis/mayor-24e/warmhouse-tamperature-service/1.0.0)

[Heating Relay Service](https://app.swaggerhub.com/apis/mayor-24e/warmhouse-heating-service/1.0.0)

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


