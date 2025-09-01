CMAKE_MINIMUM_REQUIRED ( VERSION 3.5 )

PROJECT ( "YT_CHATSERVER" )
 
SET( DIR_HOME . )

set( DIR_UTILITY ./library/utility )
SET( DIR_OUTPUT ${DIR_HOME}/../output/binary )

FILE ( GLOB SRC_FILES_HOME ${DIR_HOME}/*.cpp ${DIR_HOME}/*.cc ${DIR_HOME}/*.hpp )
FILE ( GLOB SRC_FILES_UTILITY ${DIR_UTILITY}/*.cpp ${DIR_UTILITY}/*.cc ${DIR_UTILITY}/*.hpp )
SET ( SRC_FILES ${SRC_FILES_HOME} )


SET ( LIBRARY_ROOT     ./library )

SET ( UTIL_ROOT        ${LIBRARY_ROOT}/utility )


INCLUDE_DIRECTORIES ( ${UTIL_ROOT} )


LINK_DIRECTORIES( /usr/lib )
LINK_DIRECTORIES( ${UTIL_ROOT} )



SET ( CMAKE_C_COMPILER g++ )
SET ( CMAKE_CXX_STANDARD 14 )
ADD_COMPILE_OPTIONS ( -fshort-wchar )
ADD_COMPILE_OPTIONS ( -fpermissive )
ADD_COMPILE_OPTIONS ( -Wall -Wno-unused-local-typedefs -Wno-unused-variable -Wno-unused-function -Wno-unused-parentheses )


SET ( OUTPUT "${CMAKE_PROJECT_NAME}" )

ADD_EXECUTABLE( ${OUTPUT} ${SRC_FILES} )

