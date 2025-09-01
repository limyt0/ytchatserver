#!/bin/bash

echo 'docker run'

cd /root/test/ytchat/test_yt_chat
docker run -itd --name testytchat -p 12345:2345 -v ./cppserver:/workspace/cppserver testytchat:latest /bin/bash

echo 'docker exec'
docker exec -d testytchat /workspace/cppserver/build/YT_CHATSERVER
echo 'chat start'