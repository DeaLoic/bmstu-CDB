# Database course project
BMSTU 2020-2021. 6th Semester IU7

### Launch:
cd Deploy <br>
docker-compose up

sudo fprobe  -iany -fip -B4096 -r2 -q10000 -t10000:10000000 0.0.0.0:2056


0.0.0.0:8234 - clickhouse <br>
0.0.0.0:2056 - netflow v5 <br>
0.0.0.0:2055 - netflow v7, v9

### Цель работы
Реализовать сбор и просмотр NetFlow пакетов.

### Требования
1. Сбор NetFlow пакетов.
2. Просмотр с возможностью фильтрации NetFlow пакетов.
3. Clickhouse + kafka
4. Добавление/удаление пользователей, разделение ролей.

### Figma
---

### Use-case диаграмма системы
![Use-case](Report/images/use-case.png)


### ER-диаграмма сущностей системы

![ER-web](Report/images/ER.png)

