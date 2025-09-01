//#include "stdafx.hpp"
#include "cppserver.h"


int main(int argc, char** argv)
{
    try {
        YTSManager.Init();
    } catch (const std::system_error& e) {
        std::cerr << "Caught std::system_error:" << std::endl;
        std::cerr << "  what(): " << e.what() << std::endl;
        std::cerr << "  code(): " << e.code().value() << std::endl; // 오류 코드의 실제 정수 값
        std::cerr << "  category(): " << e.code().category().name() << std::endl; // 오류 카테고리 이름
        std::cerr << "  message from code: " << e.code().message() << std::endl; // error_code 객체로부터 메시지
        return 1; // 오류 종료
    } catch (const std::exception& e) {
        std::cerr << "Caught generic exception: " << e.what() << std::endl;
        return 1;
    }
	return 0;
}