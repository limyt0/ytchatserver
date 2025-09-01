#include <iostream>
#include <string>
#include <memory> 
#include <mutex>  

namespace Utility {

template <typename T>
class YSingleton {
protected:

    YSingleton() {
   
    }

    virtual ~YSingleton() {
    
    }

private:
  
    YSingleton(const YSingleton&) = delete;
    YSingleton& operator=(const YSingleton&) = delete;

public:
    static T& GetInstance() {
        static T instance;
        return instance;
    }

   
    static void DestroyInstance() {
    }
};

} // namespace Utility