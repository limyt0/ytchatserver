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

         class RoomManager : public Utility::YSingleton<RoomManager> {
             private:
                 friend class Utility::YSingleton<RoomManager>;
             public :
                 RoomManager();
                 virtual ~RoomManager();
                 int Init();  
                 void Roomcreate(const int num);   
             public : 
                      
         };
    }
        
}
    
#define YTRoomManager YT::Server::RoomManager::GetInstance()

