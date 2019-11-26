## Stage 3: RabbitMQ / Dapper

### Learning
- RabbitMQ: https://app.pluralsight.com/library/courses/rabbitmq-by-example/table-of-contents (this is getting a bit old now, so supplement with https://www.rabbitmq.com/getstarted.html)
- Dapper: https://github.com/StackExchange/Dapper

### Tasks
- Database: Add a secondary key to the table you are using.
- DataLayer: Replace ADO.NET with Dapper.
- Service: When you update/insert data, send the data to a Queue on RabbitMQ. Have a consumer process the data and inserting it using your current WEB API. The UI should not call the WEB API directly for updates/inserts anymore, only for Gets.
The consumer should be able to run as a Windows Service or as a Command Line. This should be implemented using Topshelf. This consumer will process the updates/inserts from the UI.
The service should be able to Get data by this secondary ID. You have to solve the problem caused by the 2 Get by ID calls.

### Prerequisites
- Docker Desktop. https://www.docker.com/products/docker-desktop
- RabbitMQ
```
docker run --rm -d --hostname localhost --name my-rabbit --rm -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```