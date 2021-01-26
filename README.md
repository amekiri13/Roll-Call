# 点名器 Alpha V1.0
此点名器本着友好的原则，而为老师或老板们开发的一个非常实用的工具。希望各位老师喜欢！
## 提示
1. 语音功能还在开发，因此此调试版无此功能
2. 名单文件的字符编码集请使用ANSI，请不要使用Unicode，否则会乱码
## 使用方法
1. 第一步 启动程序，之后可以看到一个页面。
* ![](img/1.png)
2. 之后可以看到此程序出现了两个文件，分别是"config.ini"和"names.txt"，其中"config.ini"是配置文件，不能乱改，而"names.txt"为名单。
* ![](img/2.png)
3. 打开"names.txt"文件，可以看到以下内容：
* ![](img/3.png)
* 由于本名单为ini文件，所以需要遵守ini文件的语法
* 其中对一些值的介绍
  - name：为学生的姓名
  - sex：为学生的性别，M为男性，F为女性。如果输入其他，软件会显示”其他“
  - class：为学生的班级
  - ID：为学生的学号
  - ImagePath：为学生相关图片，会作为头像显示。需要保证文件路径没有问题，否则图片无法显示
* 如果需要添加学生，则需要增加节点。如初始化文件的节点是Person0，则再写个Person1，如：
* ![](img/4.png)
* 其中Person0就是第一个学生，Person1则是第二个学生
* **注意**
* 节点可以填写其他的（名称随意），但不可以出现两个一样的。
* 如:
* ![](img/5.png)
* 而以下是错误的：
* ![](img/6.png)
