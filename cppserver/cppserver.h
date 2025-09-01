//#pragma once

#include "stdafx.hpp"

//#include <iostream>

//#include <errno.h>
//#include <stdlib.h>

//#include <fcntl.h> // for fcntl, O_NONBLOCK
 // for errno
//#include <unistd.h> // for close
//#include <arpa/inet.h> //inet_ntoa
//#include <netinet/in.h> 
//#include <sys/epoll.h> // epoll_create1, epoll_ctl, epoll_event, epoll_wait
//#include <sys/socket.h> // socket, bind, litsen, accept,socklen_t, recv
//#include <sys/types.h> 
#include <array>
#include <map>
#include <set>
#include <vector>
#include "util_ysingleton.h"

namespace YT{
     namespace Server{

         class SManager : public Utility::YSingleton<SManager> {
             private:
                 friend class Utility::YSingleton<SManager>;
             public :
                 SManager();
                 virtual ~SManager();
                 int Init();    
                 void error_exit(const char* msg);
                 int set_nonblock(int fd);
                 void configure_server_socket(int serverSocket);
                 void register_in_epoll(int sock, int epollId);
                 void register_new_client(int serverSocket, int epollId);
                 void serve_client(int clientSocket);
                 void send_messages();            
         };
    }
        
}
    
#define YTSManager YT::Server::SManager::GetInstance()

