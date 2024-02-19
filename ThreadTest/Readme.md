# Readme

## 실행

```sh
docker-compose up --build
```

## locust

```sh
cd locust
locust -f locust.py  --host http://localhost --users 1000 --spawn-rate 1000
```

## monitoring

```sh
docker exec -it threadtest-web-1 /tools/dotnet-counters monitor --process-id 1
```
