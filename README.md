# Умный сервер

## Требования

* Git
* Docker
* Docker Compose

Проверить Docker:

```bash
docker --version
docker compose version
```

## 1. Скачать проект

```bash
git clone [<ССЫЛКА_НА_РЕПОЗИТОРИЙ>](https://github.com/symbolic223/Babaxaem)
```

Перейти в папку проекта:

```bash
cd Babaxaem/Babaxaem
```

## 2. Запустить проект

```bash
docker compose up --build
```

При первом запуске Docker автоматически:

* создаст контейнер PostgreSQL;
* создаст базу данных;
* запустит API;
* применит миграции;
* создаст начальные данные.

## 3. Открыть Swagger

После запуска открыть:

```text
http://localhost:5084/swagger
```

Здесь можно проверить API и отправлять запросы.

## 4. Подключение фронтенда

API доступен по адресу:

```text
http://localhost:5084
```

## 5. Остановить проект

```bash
docker compose down
```

Данные базы данных при этом сохраняются.

## 6. Перезапустить проект

```bash
docker compose down
docker compose up --build
```

## 7. Посмотреть логи

Логи API:

```bash
docker compose logs -f api
```

Логи PostgreSQL:

```bash
docker compose logs -f postgres
```

## 8. Полностью очистить базу

**Внимание: команда удалит все данные базы данных.**

```bash
docker compose down -v
docker compose up --build
```

Используйте это только если нужно начать с чистой базы.

## 9. Если проект не запускается

Попробуйте:

```bash
docker compose down
docker compose build --no-cache
docker compose up
```

Если ошибка осталась, посмотрите логи:

```bash
docker compose logs -f api
```

и отправьте их нейронке (потому что разработчик малолетний дэбил).
