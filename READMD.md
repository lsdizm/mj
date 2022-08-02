[서버 방화벽 오픈 - 오라클 리눅스 (centos)]
     sudo systemctl firewalld stop
     sudo vi /etc/firewalld/zones/public.xml
     sudo systemctl firewalld start


[서버 배포 - 서비스 재확인]
     serverice 만들어서 실행
     sudo cp mj-worker.service /etc/systemd/system/mj-worker.service
     sudo cp mj-service.service /etc/systemd/system/mj-service.service

     sudo systemctl daemon-reload
     sudo systemctl enable mj-worker.service;sudo systemctl enable mj-service.service

     sudo systemctl stop mj-worker.service;sudo systemctl stop mj-service.service;sudo systemctl start mj-worker.service;sudo systemctl start mj-service.service
    
     sudo systemctl status mj-worker.service 

[개발 완료후 배포 패키지 준비]
     dotnet publish -c Release -o ../mj.publish 
     -- 이후 push 한다음 서버에서 받아서 서비스 재시작

[개발시 참조 추가]
     sudo dotnet add package Dapper
     sudo dotnet add package Mysql.Data
     dotnet add reference ../mj.model/mj.model.csproj
     