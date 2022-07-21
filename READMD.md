2022-07-19
     sudo dotnet publish -c Release
     
     serverice 만들어서 실행
     sudo cp mj-worker.service /etc/systemd/system/mj-worker.service
     
     sudo systemctl daemon-reload
     sudo systemctl enable mj-worker.service
     sudo systemctl start mj-worker.service
     sudo systemctl stop mj-worker.service
     sudo systemctl status mj-worker.service 


     sudo dotnet add package Dapper
     sudo dotnet add package Mysql.Data

2022-07-20
     솔루션
     dotnet publish -c Release -o ../mj.publish 