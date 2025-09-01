#!/bin/bash

if [ $(docker ps | grep testytchat -c) -gt 0 ]; then docker stop testytchat; fi
if [ $(docker ps -a | grep testytchat -c) -gt 0 ]; then docker rm testytchat; fi

cd /root/test/ytchat/test_yt_chat
echo 'docker build'
docker build --tag testytchat:latest --file Dockerfile_test_ytchat_u20.04 .


echo 'docker run ytchat'
docker run -itd --name testytchat -p 12345:2345 -v ./cppserver:/workspace/cppserver testytchat:latest /bin/bash

echo 'docker exec ytcmake'
docker exec testytchat /workspace/cppserver/ytcmake.sh

echo 'docker stop'
docker stop testytchat
docker rm testytchat
