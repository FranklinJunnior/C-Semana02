
docker build -t back .

sleep 2
clear

sleep 1

docker run -d -it -p 5000:8080 --name apiend back


cd /frontend

chmod +x ixic.sh 

./ixic.sh


