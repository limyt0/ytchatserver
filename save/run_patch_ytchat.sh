#!/bin/bash

echo 'docker stop'

if [ $(docker ps | grep testytchat -c) -gt 0 ]; then docker stop testytchat; fi
if [ $(docker ps -a | grep testytchat -c) -gt 0 ]; then docker rm testytchat; fi

echo 'docker run'
# cd /root/test
# docker run -itd --name testytchat -p 12345:2345 testytchat:latest /bin/bash

echo 'docker stop'
# docker stop testytchat
# docker rm testytchat