#!/bin/bash

echo 'dir'
cd /workspace/cppserver
mkdir -p build
chmod 775 -R build
cd /workspace/cppserver/build 

echo 'cmake'
cmake /workspace/cppserver
echo 'cmake complete'

echo 'make'
make
echo 'make complete'