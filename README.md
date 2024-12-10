![Screenshot 2024-11-06 at 13 04 34](https://github.com/user-attachments/assets/9017703a-d7d5-4092-b27a-93dd498e3415)


# Instruction
        
| Service              | Status  |
|----------------------|---------|
| Catalog service      | Success |
| Order service        | Success |
| Basket service       | Success |
| Identity service     | Success |
| Notification service | Pending |
| Search service       | Pending |
| Web client           | Pending |


#  How to run project
* Create folder certs and create file https.pfx (for https into communication grpc)
    * Using dev-certs tools dotnet
        ```shell
        dotnet dev-certs -ep ./certs/https.pfx -p <Your_password>
        ```
    * Using openssl
        ```shell

        ```
* Create file `.env` with your environments
    ```dotenv
    MSSQL_PASSWORD=@P@ssw0rd02
    Dabatabase_Name=sqlserver
    
    
    ConnectionStrings__Db=Server=sqlserver;Database=Db;Encrypt=false;User Id=sa;Password=@P@ssw0rd02
    SchemaRegistry__Url=http://schema-registry:8085
    
    ```

And after run command into terminal
```shell
docker compose -f docker-compose.yml up -d
```
#  Review code with SonarQube
Url: https://de7c-113-190-242-151.ngrok-free.app/
Uname/upass: cshopuser/Admin123123!@
