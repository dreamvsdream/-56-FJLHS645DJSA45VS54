MyWebServer是一个高性能、易用、小巧、绿色的轻量级WEB服务器软件，是你快速建站及个人HTTP文件服务器的难得工具。支持HTTP/1.1、断点续传、大文件下载、正则表达式URL重写、虚拟目录、HTTP反向代理、gzip压缩等功能,内置ASP有限支持，可通过ISAPI接口、FastCGI接口实现执行服务器脚本（如PHP,asp,asp.net等），性能完全超越IIS等很多主流WEB服务器软件。

***特别说明:由于3.0版之后URL重写规则命令变化较大,请自行修改旧规则***

使用说明:
　　使用FastCGI时，在映射设置中将映射模块设置为启动FastCGI的命令，且命令行中必须包含IP:port格式（如：127.0.0.1:8988）的服务器信息，当不需要WEB服务器启动FastCGI时，命令行中填入IP:port格式的FastCGI服务器信息即可。
如果使用ISAPI接口，指定ISAPI的DLL文件即可,使用网页压缩功能需gzip.dll文件支持。

注(本服务器内置ASP支持,支持UTF-8和ANSI编码，其它脚本需要使用请自行安装)：  PHP通过isapi和FastCGI接口均可(isapi方式建议使用PHP 5.2,因为5.3以上版不再提供ISAPI支持)；asp.net支持可安装mono然后通过FasctCGI接口实现。上述脚本已测试过可以运行。

　　URL重写命令（使用基于VBScript的正则表达式）：
　　ifsve  如果匹配指定的服务器变量则往下执行，否则执行下条规则之后的规则。（支持HTTP_HOST REMOTE_ADDR HTTP_REFERER　URL四个服务器变量）
    rewrite 如果匹配URL 则执行重写后面URL操作,可选命令参数: P 执行反向代理; R 执行重定向; L 最后一条规则; D 禁止URL并返回HTTP状态码 例如:rewrite ^/test.rar /web/test.rar L  
　　sethd  修改反向代理时发送的HTTP头值 格式为 sethd=头名称:头值，头名称区分大小写。

小技巧：通过URL重写可让WEB服务器变成一台HTTP代理服务器(代理上网)，服务端口就是代理端口，重写规则如下：  
rewrite ^(http:.*) $1 P


特别声明:本版本软件包中的所有DLL文件为第三方软件部件,文件来源于网络,不属于本软件的一部分,也不保证其安全性和稳定性,仅作为测试使用,不对其产生的后果负责.
 


